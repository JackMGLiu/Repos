﻿namespace NetCoreService.DTO.FormatModel
{
    public class TreeModel
    {
        public string id { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string parentid { get; set; }
        public int? isnav { get; set; }
        public int? islast { get; set; }
    }
}
