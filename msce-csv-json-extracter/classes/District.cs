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
     * model class for a single district
     */
    class District
    {
        private string name;//district name
        private string region;//region name
        private string regionId;//region ID

        public District()//default constructor
        {
        }

        public District(string name, string region, string regionId)
        {
            this.name = name;
            this.region = region;
            this.regionId = regionId;
        }

        public void setDistrictName(string name)
        {
            this.name = name;
        }

        public void setRegion(string region)
        {
            this.region = region;
        }

        public void setRegionId(string regionId)
        {
            this.regionId = regionId;
        }

        public string getDistrictName()
        {
            return this.name;
        }

        public string getRegion()
        {
            return this.region;
        }

        public string getRegionId()
        {
            return regionId;
        }

        /* returns a string in the representation of a JSON object */
        public string toJSONString()
        {
            return String.Format("{\"name\":\"{0}\", \"region\":\"{1}\", \"region_id\":\"{2}\"}",getDistrictName(),getRegion(),getRegionId());
        }

        /* returns a JObject */
        public JObject toJSONObject()
        {
            JObject district = new JObject();
            district.Add(AppConstants.TAG_DISTRICT_NAME, getDistrictName());
            district.Add(AppConstants.TAG_REGION, getRegion());
            district.Add(AppConstants.TAG_REGION_ID, getRegionId());
            return district;
        }
    }
}
