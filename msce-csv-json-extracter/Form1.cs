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
     * version 0.9.0.1
     */
    public partial class frmMain : Form
    {
        private String filePath = "";//stores current file path
        
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
                //lblFilePath.Text = ofCSVDialog.FileName;//set file path  
                String allPaths = "";
                foreach (String path in ofCSVDialog.FileNames) {
                    allPaths += "\n-> " + path;
                }
                lblFilePath.Text = allPaths;
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
                    Regex dRegex = new Regex(classes.AppConstants.REGEX_MSCE_DISTRICT_NAME, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = dRegex.Matches(oneLine);
                    if (matches.Count == 1) {//single match been found
                        Console.WriteLine("matched district -> " + oneLine);
                        appendToDistricts(oneLine);
                    }
                    #endregion district

                    /* match msce result */
                    #region result
                    Regex r2Regex = new Regex(classes.AppConstants.REGEX_MSCE_RESULTS_2, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    Regex r1Regex = new Regex(classes.AppConstants.REGEX_MSCE_RESULTS_1, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = r2Regex.Matches(oneLine);
                    if (matches.Count == 1)//single match with two results
                    {
                        Console.WriteLine("matched dual result -> "+oneLine);
                        appendToCandidates(oneLine, true);
                    }
                    else
                    {
                        /* try matching one result */
                        matches = r1Regex.Matches(oneLine);

                        if (matches.Count == 1)
                        {
                            Console.WriteLine("matched single result -> " + oneLine);
                            appendToCandidates(oneLine, false);
                        }

                    }
                    #endregion result

                    /* match centre number */
                    #region centreNumber
                    Regex cNoRegex = new Regex(classes.AppConstants.REGEX_MSCE_CENTRE_NO, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = cNoRegex.Matches(oneLine);
                    if (matches.Count == 1)
                    {
                        Console.WriteLine("matched centre number -> " + oneLine);                
                    }
                    #endregion centreNumber

                    /* match centre name */
                    #region centreName
                    Regex cNaRegex = new Regex(classes.AppConstants.REGEX_MSCE_CENTRE_NAME, RegexOptions.Compiled | RegexOptions.IgnoreCase);

                    matches = cNaRegex.Matches(oneLine);
                    if(matches.Count == 1)
                    {
                        Console.WriteLine("matched center name -> " + oneLine);
                    }
                    #endregion centreName

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("extractResults() Exception -> "+ex.Message);
            }
        }

        private bool appendToDistricts(String district) {
            return false;
        }

        private bool appendToCandidates(String candidate,bool isDual) {
            return false;
        }

        private bool appendToCentres(String centre) {
            return false;
        }

        private bool appendToSchools(String school) {
            return false;
        }

        private void btnExtract_Click(object sender, EventArgs e)
        {
            extractResults(ofCSVDialog.FileName);//extract results from csv file
        }
    }
}
