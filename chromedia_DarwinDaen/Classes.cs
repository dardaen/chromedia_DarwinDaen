using System;
using System.Collections.Generic;

namespace chromedia_DarwinDaen
{
    public class ResponseData
    { 
        public int page { get; set; }
        public int per_Page { get; set; }
        public int total { get; set; }
        public int total_Pages { get; set; }
        public List<DetailedData> data { get; set; }


    }

    public class DetailedData
    {

        public string title { get; set; }
        public string url { get; set; }
        public string author { get; set; }
        public int? num_comments { get; set; }
        public string story_id { get; set; }
        public string story_title { get; set; }
        public string story_url { get; set; }
        public string parent_id { get; set; }
        public string created_at { get; set; }

    }
}
