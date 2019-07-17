using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using msce_csv_json_extracter.classes;


namespace msce_csv_json_extracter.classes
{
    /* Created by Chief Wiz on 16-07-2019
     * model class for a candidate's result
     */

    class Candidate
    {
        private string number;
        private string centreCode;
        private string centreId;
        private string gender;
        private string name;


        public Candidate()
        {
        }

        public Candidate(string number, string centreCode, string centreId, string gender, string name)
        {
            this.number = number;
            this.centreCode = centreCode;
            this.centreId = centreId;
            this.gender = gender;
            this.name = name;
        }

        public void setCandidateNumber(string number)
        {
            this.number = number;
        }

        public void setCentreCode(string code)
        {
            this.centreCode = code;
        }

        public void setCentreId(string centreId)
        {
            this.centreId = centreId;
        }

        public void setGender(string gender)
        {
            this.gender = gender;
        }

        public void setCandidateName(string name)
        {
            this.name = name;
        }

        public string getCandidateNumber()
        {
            return this.number;
        }

        public string getCentreCode()
        {
            return this.centreCode;
        }

        public string getCentreId()
        {
            return this.centreId;
        }

        public string getGender()
        {
            return this.gender;
        }

        public string getCandidateName()
        {
            return this.name;
        }

        public JObject toJSONObject()
        {
            JObject candidate = new JObject();
            candidate.Add(AppConstants.TAG_CANDIDATE_NAME, getCandidateName());
            candidate.Add(AppConstants.TAG_CANDIDATE_NUMBER, getCandidateNumber());
            candidate.Add(AppConstants.TAG_GENDER, getGender());
            candidate.Add(AppConstants.TAG_CENTRE_CODE, getCentreCode());
            candidate.Add(AppConstants.TAG_CENTRE_ID, getCentreId());
            return candidate;
        }
    }
}
