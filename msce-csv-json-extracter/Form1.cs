using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using msce_csv_json_extracter.classes;
/*
* Copyright (c) 2019 Lomaku Technologies.
* All Rights Reserved.
* 40 Estate Road, Angelo Goveya, Limbe.
* P.O. Box 30653, Lilongwe 3.
* +265 999 388 747
* +265 888 676 896
* https://github.com/LomakuIT
* admin@lomakuit.com
*
* Redistribution and use in source and binary forms, with or without modification,
* are permitted provided that the following conditions are met:
*
* 1. Redistributions of source code must retain the above copyright notice,
* this list of conditions and the following disclaimer.
*
* 2. Redistributions in binary form must reproduce the above copyright notice,
* this list of conditions and the following disclaimer in the documentation
* and/or other materials provided with the distribution.
*
* 3. Neither the name of the copyright holder nor the names of its contributors
* may be used to endorse or promote products derived from this software
* without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
* AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO,
* THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
* ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
* INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
* PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
* HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
* OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE,
* EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE
*/
namespace msce_csv_json_extracter
{
    /* Created by Chief Wiz on 05-06-2019
     * CSV to JSON extraction tool for MSCE results data 
     * version 0.9.0.3
     */
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (ofCSVDialog.ShowDialog() == DialogResult.OK)
            {
                /* extract result for all CSV files */
                String allPaths = "";
                foreach (String path in ofCSVDialog.FileNames) {
                    allPaths += "-> " + path + "\n";
                }
                lblFilePath.Text = allPaths;
            }
            else
            {
                Console.WriteLine("Dialog picker not okay");
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();//close application

        }

        private void extractResults(String filepath) {
            try
            {
                MatchCollection matches;
                StreamReader sr = new StreamReader(filepath);
                while (!sr.EndOfStream)
                {
                    var oneLine = sr.ReadLine();//read single line from csv

                    /* match district */
                    #region district
                    Regex dRegex = new Regex(AppConstants.REGEX_MSCE_DISTRICT_NAME, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = dRegex.Matches(oneLine);
                    if (matches.Count == 1) {//single match been found
                        Console.WriteLine("matched district -> " + oneLine);
                    
                        foreach (Match m in matches)
                        {
                            District mDistrict = new District(m.Groups[AppConstants.TAG_DISTRICT_CODE_GROUP].Value, "SOUTH", null);//default region is SOUTH
                        //    Console.WriteLine("named district JObject-> " + mDistrict.toJSONObject());
                            appendToDistricts(mDistrict);
                        }
                    }
                    #endregion district

                    /* match msce result */
                    #region result
                    Regex r2Regex = new Regex(AppConstants.REGEX_MSCE_RESULTS_2, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex r1Regex = new Regex(AppConstants.REGEX_MSCE_RESULTS_1, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = r2Regex.Matches(oneLine);

                    if (matches.Count == 1)//single match with two candidate results
                    {
                        Console.WriteLine("matched dual result -> " + oneLine);

                        foreach (Match m in matches)
                        {
                            #region firstCandidate
                            Candidate candidate = new Candidate();
                            candidate.setCandidateNumber(m.Groups[AppConstants.TAG_CANDIDATE_CODE_GROUP].Value.Replace(" ",""));
                            candidate.setCandidateName(m.Groups[AppConstants.TAG_FIRSTNAME_CODE_GROUP].Value +" "+ m.Groups[AppConstants.TAG_SURNAME_CODE_GROUP].Value);
                            candidate.setGender(m.Groups[AppConstants.TAG_GENDER_CODE_GROUP].Value);
                            candidate.setCentreId(null);//normally null as no indices at this point
                            candidate.setCentreCode(candidate.getCandidateNumber().Substring(0, 5));//extract centre code from candidate number

                            appendToCandidates(candidate);//append to candidates
                         //   Console.WriteLine("first candidate extracted JSON -> " + candidate.toJSONObject());
                            #endregion firstCandidate

                            #region secondCandidate
                            Candidate candidate1 = new Candidate();
                            candidate1.setCandidateNumber(m.Groups[AppConstants.TAG_CANDIDATE1_CODE_GROUP].Value.Replace(" ",""));
                            candidate1.setCandidateName(m.Groups[AppConstants.TAG_FIRSTNAME1_CODE_GROUP].Value + " " + m.Groups[AppConstants.TAG_SURNAME1_CODE_GROUP].Value);
                            candidate1.setGender(m.Groups[AppConstants.TAG_GENDER1_CODE_GROUP].Value);
                            candidate1.setCentreId(null);//normally null as no indices at this point
                            candidate1.setCentreCode(candidate1.getCandidateNumber().Substring(0, 5));//extract centre code from candidate number

                            appendToCandidates(candidate1);//append to candidates
                         //   Console.WriteLine("second candidate extracted JSON -> " + candidate1.toJSONObject());
                            #endregion secondCandidate
                        }
                        
                    }
                    else
                    {
                        /* try matching one result */
                        matches = r1Regex.Matches(oneLine);

                        if (matches.Count == 1)//single match with single candidate results
                        {
                            Console.WriteLine("matched single result -> " + oneLine);

                            foreach (Match m in matches)
                            {
                                #region oneCandidate
                                Candidate candidate = new Candidate();
                                candidate.setCandidateNumber(m.Groups[AppConstants.TAG_CANDIDATE_CODE_GROUP].Value.Replace(" ",""));
                                candidate.setCandidateName(m.Groups[AppConstants.TAG_FIRSTNAME_CODE_GROUP].Value + " " + m.Groups[AppConstants.TAG_SURNAME_CODE_GROUP].Value);
                                candidate.setGender(m.Groups[AppConstants.TAG_GENDER_CODE_GROUP].Value);
                                candidate.setCentreId(null);//normally null as no indices at this point
                                candidate.setCentreCode(candidate.getCandidateNumber().Substring(0, 5));//extract centre code from candidate number

                                appendToCandidates(candidate);//append to candidates
                             //   Console.WriteLine("only candidate extracted JSON -> " + candidate.toJSONObject());
                                #endregion oneCandidate

                                } 
                            }

                        }
                    #endregion result

                    /* match centre number */
                    #region centreNumber
                    Regex cNoRegex = new Regex(AppConstants.REGEX_MSCE_CENTRE_NO, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = cNoRegex.Matches(oneLine);
                    if (matches.Count == 1)
                    {
                        Console.WriteLine("matched centre number -> " + oneLine);
                    }
                    #endregion centreNumber

                    /* match centre name */
                    #region centreName
                    Regex cNaRegex = new Regex(AppConstants.REGEX_MSCE_CENTRE_NAME, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = cNaRegex.Matches(oneLine);
                    if (matches.Count == 1)
                    {
                        Console.WriteLine("matched center name -> " + oneLine);
                    }
                    #endregion centreName
                    
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("extractResults() Exception -> " + ex.Message);
                System.Diagnostics.Debug.WriteLine("extractResults() Exception stackTrace -> " + ex.StackTrace);
               
            }
        }

        private bool appendToDistricts(District district) { 
           
            return false;
        }

        private bool appendToCandidates(Candidate candidate) {
            return false;
        }

        private bool appendToCentres(Centre centre) {
            return false;
        }

        private bool appendToSchools(School school) {
            return false;
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
          //  extractResults(ofCSVDialog.FileName);//extract results from single csv file
            foreach (String path in ofCSVDialog.FileNames)
            {
                extractResults(path);//extract all results from CSV
            }
        }

        private void developersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void districtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
           

        }

        private void schoolsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void candidateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void centersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
