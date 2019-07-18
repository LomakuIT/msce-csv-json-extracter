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
     * version 0.9.0.6
     */
    public partial class frmMain : Form
    {
        private JArray candidatesArray;
        private JArray districtsArray;
        private JArray schoolsArray;
        private JArray centresArray;
        private int candidateCount = 0, centreCount = 0, districtCount = 0, schoolCount = 0;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            initResources();
        }

        /* initialise resources such as data structures */
        private void initResources()
        {
            candidatesArray = new JArray();
            centresArray = new JArray();
            districtsArray = new JArray();
            schoolsArray = new JArray();
        }
        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            if (ofCSVDialog.ShowDialog() == DialogResult.OK)
            {
                /* extract result for all CSV files */
                String allPaths = "";
                foreach (String path in ofCSVDialog.FileNames)
                {
                    allPaths += "-> " + path + "\n";
                }
                lblFilePath.Text = allPaths;
            }
            else
            {
                Console.WriteLine("Dialog picker not okay");
                MessageBox.Show(AppConstants.MESSAGE_FILE_DIALOG_NONE_SELECTED, AppConstants.CAPTION_FILE_DIALOG_NONE_SELECTED, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();//close application

        }

        private void extractResults(String filepath)
        {
            try
            {
                MatchCollection matches;
                StreamReader sr = new StreamReader(filepath);
                Centre centre = new Centre();

                while (!sr.EndOfStream)
                {
                    var oneLine = sr.ReadLine();//read single line from csv

                    /* match msce result */
                    #region result
                    Regex r2Regex = new Regex(AppConstants.REGEX_MSCE_RESULTS_2, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex r1Regex = new Regex(AppConstants.REGEX_MSCE_RESULTS_1, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = r2Regex.Matches(oneLine);

                    if (matches.Count == 1)//single match with two candidate results
                    {

                        foreach (Match m in matches)
                        {
                            #region firstCandidate
                            Candidate candidate = new Candidate();
                            candidate.setCandidateNumber(m.Groups[AppConstants.TAG_CANDIDATE_CODE_GROUP].Value.Replace(" ", ""));
                            candidate.setCandidateName(m.Groups[AppConstants.TAG_FIRSTNAME_CODE_GROUP].Value + " " + m.Groups[AppConstants.TAG_SURNAME_CODE_GROUP].Value);
                            candidate.setGender(m.Groups[AppConstants.TAG_GENDER_CODE_GROUP].Value);
                            candidate.setCentreId(null);//normally null as no indices at this point
                            candidate.setCentreCode(candidate.getCandidateNumber().Substring(0, 5));//extract centre code from candidate number

                            appendToCandidates(candidate);//append to candidates
                            #endregion firstCandidate

                            #region secondCandidate
                            Candidate candidate1 = new Candidate();
                            candidate1.setCandidateNumber(m.Groups[AppConstants.TAG_CANDIDATE1_CODE_GROUP].Value.Replace(" ", ""));
                            candidate1.setCandidateName(m.Groups[AppConstants.TAG_FIRSTNAME1_CODE_GROUP].Value + " " + m.Groups[AppConstants.TAG_SURNAME1_CODE_GROUP].Value);
                            candidate1.setGender(m.Groups[AppConstants.TAG_GENDER1_CODE_GROUP].Value);
                            candidate1.setCentreId(null);//normally null as no indices at this point
                            candidate1.setCentreCode(candidate1.getCandidateNumber().Substring(0, 5));//extract centre code from candidate number

                            appendToCandidates(candidate1);//append to candidates
                            #endregion secondCandidate
                        }

                    }
                    else
                    {
                        /* try matching one result */
                        matches = r1Regex.Matches(oneLine);

                        if (matches.Count == 1)//single match with single candidate results
                        {

                            foreach (Match m in matches)
                            {
                                #region oneCandidate
                                Candidate candidate = new Candidate();
                                candidate.setCandidateNumber(m.Groups[AppConstants.TAG_CANDIDATE_CODE_GROUP].Value.Replace(" ", ""));
                                candidate.setCandidateName(m.Groups[AppConstants.TAG_FIRSTNAME_CODE_GROUP].Value + " " + m.Groups[AppConstants.TAG_SURNAME_CODE_GROUP].Value);
                                candidate.setGender(m.Groups[AppConstants.TAG_GENDER_CODE_GROUP].Value);
                                candidate.setCentreId(null);//normally null as no indices at this point
                                candidate.setCentreCode(candidate.getCandidateNumber().Substring(0, 5));//extract centre code from candidate number

                                appendToCandidates(candidate);//append to candidates
                                                              //  Console.WriteLine("only candidate extracted JSON -> " + candidate.toJSONObject());
                                #endregion oneCandidate

                            }
                        }

                    }
                    #endregion result

                    /* match centre name */
                    #region centreName
                    Regex cNaRegex = new Regex(AppConstants.REGEX_MSCE_CENTRE_NAME, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = cNaRegex.Matches(oneLine);
                    if (matches.Count == 1)
                    {
                        centre = new Centre();//new centre

                        foreach (Match m in matches)
                        {
                            String c = m.Groups[AppConstants.TAG_CENTRE_CODE_GROUP].Value;
                            centre.setCentreName(c);//set centre name
                        }
                    }
                    #endregion centreName

                    /* match centre number */
                    #region centreNumber
                    Regex cNoRegex = new Regex(AppConstants.REGEX_MSCE_CENTRE_NO, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = cNoRegex.Matches(oneLine);
                    if (matches.Count == 1)
                    {
                        foreach (Match m in matches)
                        {
                            String c = m.Groups[AppConstants.TAG_CENTRE_NUMBER_CODE_GROUP].Value;
                            String code = String.Format("{0}{1}", "M", c);//format centre code to appropriate centre code
                            centre.setCentreCode(code);//set centre code
                        }

                    }
                    #endregion centreNumber

                    /* match district */
                    #region district
                    Regex dRegex = new Regex(AppConstants.REGEX_MSCE_DISTRICT_NAME, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = dRegex.Matches(oneLine);
                    if (matches.Count == 1)
                    {//single match been found

                        foreach (Match m in matches)
                        {
                            District mDistrict = new District();
                            String fDistrict = m.Groups[AppConstants.TAG_DISTRICT_CODE_GROUP].Value;

                            if (fDistrict.Contains(AppConstants.NAME_DISTRICT_BLANTYRE) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_NSANJE) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_CHIKHWAWA) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_ZOMBA) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_PHALOMBE) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_MACHINGA) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_MANGOCHI) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_MWANZA) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_NENO) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_THYOLO) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_MULANJE) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_CHIRADZULU) ||
                                 fDistrict.Contains(AppConstants.NAME_DISTRICT_BALAKA)
                                 )
                            {
                                mDistrict = new District(fDistrict, "SOUTH", null);//if southern district is detected
                            }
                            else if (fDistrict.Contains(AppConstants.NAME_DISTRICT_DEDZA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_LILONGWE) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_SALIMA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_MCHINJI) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_KASUNGU) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_DOWA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_NTCHISI) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_NTCHEU) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_NKHOTAKOTA)
                                     )
                            {
                                mDistrict = new District(fDistrict, "CENTRAL", null);//if central district is detected
                            }
                            else if (fDistrict.Contains(AppConstants.NAME_DISTRICT_RUMPHI) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_NKHATA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_KARONGA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_CHITIPA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_MZIMBA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_LIKOMA) ||
                                     fDistrict.Contains(AppConstants.NAME_DISTRICT_MZUZU))
                            {
                                mDistrict = new District(fDistrict, "NORTH", null);//if northern district is detected
                            }

                            centre.setDistrict(mDistrict.getDistrictName());//set centre district
                            bool hasAppended = appendToDistricts(mDistrict);
                            bool hasAppended1 = appendToCentres(centre);
                            bool hasAppended2 = appendToSchools(centre);

                            Console.WriteLine("has appended district " + hasAppended);
                            Console.WriteLine("has appended centre " + hasAppended1);
                            Console.WriteLine("has appended school " + hasAppended2);
                        }
                    }

                    #endregion district

                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("extractResults() Exception -> " + ex.Message);
                System.Diagnostics.Debug.WriteLine("extractResults() Exception stackTrace -> " + ex.StackTrace);
            }
            finally
            {
                writeToJSONFiles();//write extracted data to JSON files
            }
        }

        private bool appendToDistricts(District district)
        {
            int currentCount = districtsArray.Count;//get district count before add operation
            bool existsAlready = false;

            if (districtsArray.Count > 0)
            {
                foreach (JObject dObject in districtsArray)
                {
                    //convert to string as cannot compare with JToken
                    String dName = (String)dObject.GetValue(AppConstants.TAG_DISTRICT_NAME);
                    String region = (String)dObject.GetValue(AppConstants.TAG_REGION);

                    if (district.getDistrictName().Trim().Equals(dName.Trim())
                        && district.getRegion().Trim().Equals(region.Trim())
                        )
                    {
                        existsAlready = true;
                    }
                }
            }

            //if district does not already exist
            if (!existsAlready)
            {
                districtsArray.Add(district.toJSONObject());
                districtCount++;//increment district count
            }

            //check to see if new district has been successfully added
            if (currentCount < districtsArray.Count)
            {
                return true;
            }

            return false;
        }

        private bool appendToCandidates(Candidate candidate)
        {
            int currentCount = candidatesArray.Count;//get count before add operation
            candidatesArray.Add(candidate.toJSONObject());//add received candidate

            if (currentCount < candidatesArray.Count)
            {
                candidateCount++;//increment candidate count
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool appendToCentres(Centre centre)
        {
            int currentCount = centresArray.Count;//get current centre count before add operation
            bool existsAlready = false;

            foreach (JObject cObject in centresArray)
            {
                //convert to string as cannot compare with JToken
                String cName = (String)cObject.GetValue(AppConstants.TAG_CENTRE_NAME);
                String cCode = (String)cObject.GetValue(AppConstants.TAG_CENTRE_CODE);

                //if centre name and centre code match then do not add centre as it already exists
                if (cName.Trim().Equals(centre.getCentreName().Trim())
                    && cCode.Trim().Equals(centre.getCentreCode().Trim())
                    )
                {
                    existsAlready = true;
                }

            }

            if (!existsAlready)//if centre does not already exist
            {
                centresArray.Add(centre.toJSONObject());//add centre
                centreCount++;//increment centre count
            }

            //new centre has been added
            if (currentCount < centresArray.Count)
            {
                return true;
            }

            return false;
        }

        private bool appendToSchools(Centre centre)
        {
            int currentCount = schoolsArray.Count;
            bool existsAlready = false;

            foreach (JObject sObject in schoolsArray)
            {

                //convert to string as cannot compare with JToken
                String school = (String)sObject.GetValue(AppConstants.TAG_SCHOOL_NAME);
                String district = (String)sObject.GetValue(AppConstants.TAG_DISTRICT);

                if (school.Trim().Equals(centre.getCentreName().Trim())
                    && district.Trim().Equals(centre.getDistrict().Trim())
                    )
                {
                    existsAlready = true;
                }

            }

            if (!existsAlready)
            {
                School mSchool = new School(centre.getCentreName(), centre.getDistrict(), null);//define new school
                schoolsArray.Add(mSchool.toJSONObject());
                schoolCount++;//increment school count
            }

            //if new school has been added
            if (currentCount < schoolsArray.Count)
            {
                return true;
            }

            return false;
        }

        private void writeToJSONFiles()
        {

            String candidatePath = Directory.GetCurrentDirectory() + @"\json\candidates.json";
            String schoolsPath = Directory.GetCurrentDirectory() + @"\json\schools.json";
            String centresPath = Directory.GetCurrentDirectory() + @"\json\centres.json";
            String districtsPath = Directory.GetCurrentDirectory() + @"\json\districts.json";

            try
            {
                FileInfo file;//file object used to write to file

                /* write to candidates JSON */
                #region writeCandidates
                file = new FileInfo(candidatePath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, candidatesArray.ToString());
                #endregion writeCandidates

                /* write to schools JSON */
                #region writeSchools
                file = new FileInfo(schoolsPath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, schoolsArray.ToString());
                #endregion writeSchools

                /* write to centres JSON */
                #region writeCentres
                file = new FileInfo(centresPath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, centresArray.ToString());
                #endregion writeCentres

                /* write to districts JSON */
                #region writeDistricts
                file = new FileInfo(districtsPath);
                file.Directory.Create();
                File.WriteAllText(file.FullName, districtsArray.ToString());
                #endregion writeDistricts

            }
            catch (FileNotFoundException fnfEx)
            {
                System.Diagnostics.Debug.WriteLine("writeToJSONFiles() FNF Exception -> " + fnfEx.Message);
                System.Diagnostics.Debug.WriteLine("writeToJSONFiles() FNF Exception stackTrace -> " + fnfEx.StackTrace);
            }
            catch (IOException ioEx)
            {
                System.Diagnostics.Debug.WriteLine("writeToJSONFiles() IO Exception -> " + ioEx.Message);
                System.Diagnostics.Debug.WriteLine("writeToJSONFiles() IO Exception stackTrace -> " + ioEx.StackTrace);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("writeToJSONFiles() Exception -> " + ex.Message);
                System.Diagnostics.Debug.WriteLine("writeToJSONFiles() Exception stackTrace -> " + ex.StackTrace);
            }

        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            int fileCount = ofCSVDialog.FileNames.Length;
            progressExtraction.Maximum = fileCount;
            progressExtraction.Step = 1;

            foreach (String path in ofCSVDialog.FileNames)
            {
                extractResults(path);//extract all results from CSV
                progressExtraction.PerformStep();
                
            }
            progressExtraction.Value = fileCount;
        }

        private void developersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AppConstants.MESSAGE_DEVELOPERS_APP, AppConstants.CAPTION_DEVELOPERS_APP, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void howToUseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void contactUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AppConstants.MESSAGE_CONTACT_US, AppConstants.CAPTION_CONTACT_US, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void districtsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String districtsPath = Directory.GetCurrentDirectory() + @"\json\districts.json";
            OpenFilePathInExplorer(districtsPath);
        }

        private void schoolsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String schoolsPath = Directory.GetCurrentDirectory() + @"\json\schools.json";
            OpenFilePathInExplorer(schoolsPath);
        }

        private void candidateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String candidatePath = Directory.GetCurrentDirectory() + @"\json\candidates.json";
            OpenFilePathInExplorer(candidatePath);
        }

        private void centersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String centresPath = Directory.GetCurrentDirectory() + @"\json\centres.json";
            OpenFilePathInExplorer(centresPath);
        }

        private void OpenFilePathInExplorer(String fullPath)
        {
            try
            {
                string argument = "/select, \"" + fullPath + "\"";
                System.Diagnostics.Process.Start("explorer.exe", argument);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("districtsToolStripMenuItem_Click() Exception -> " + ex.Message);
                System.Diagnostics.Debug.WriteLine("districtsToolStripMenuItem_Click() Exception stackTrace -> " + ex.StackTrace);
            }
        }
    }
}
