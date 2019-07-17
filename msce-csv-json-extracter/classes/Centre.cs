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
     * model class for a Centre
     */
    class Centre
    {
        private string name;
        private string code;
        private string district;
        private string schoolId;


        public Centre()//default constructor
        {
        }

        public Centre(string name, string code, string district, string schoolId)
        {
            this.name = name;
            this.code = code;
            this.district = district;
            this.schoolId = schoolId;
        }

        public void setCentreName(string name)
        {
            this.name = name;
        }

        public void setCentreCode(string code)
        {
            this.code = code;
        }

        public void setDistrict(string district)
        {
            this.district = district;
        }

        public void setSchoolId(string schoolId)
        {
            this.schoolId = schoolId;
        }

        public string getCentreName()
        {
            return name;
        }

        public string getCentreCode()
        {
            return code;
        }

        public string getDistrict()
        {
            return district;
        }

        public string getSchoolId()
        {
            return schoolId;
        }

        public JObject toJSONObject()
        {
            JObject centre = new JObject();
            centre.Add(AppConstants.TAG_CENTRE_NAME, getCentreName());
            centre.Add(AppConstants.TAG_CENTRE_CODE, getCentreCode());
            centre.Add(AppConstants.TAG_DISTRICT, getDistrict());
            centre.Add(AppConstants.TAG_SCHOOL_ID, getSchoolId());
            return centre;
        }
    }
}
