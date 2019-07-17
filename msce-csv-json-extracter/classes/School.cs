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
     * model class for a school
     */
    class School
    {
        private string name;
        private string district;
        private string districtId;

        public School()
        {
        }

        public School(string name, string district, string districtId)
        {
            this.name = name;
            this.district = district;
            this.districtId = districtId;
        }

        public void setSchoolName(string name)
        {
            this.name = name;
        }

        public void setDistrict(string district)
        {
            this.district = district;
        }

        public void setDistrictId(string districtId)
        {
            this.districtId = districtId;
        }

        public string getSchoolName()
        {
            return this.name;
        }

        public string getDistrict()
        {
            return this.district;
        }

        public string getDistrictId()
        {
            return this.districtId;
        }

        public JObject toJSONObject()
        {
            JObject school = new JObject();
            school.Add(AppConstants.TAG_SCHOOL_NAME, getSchoolName());
            school.Add(AppConstants.TAG_DISTRICT, getDistrict());
            school.Add(AppConstants.TAG_DISTRICT_ID, getDistrictId());
            return school;
        }
    }
}
