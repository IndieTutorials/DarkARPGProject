﻿using System;
using System.Collections.Generic;

namespace RustedGames
{
    [Serializable]
    public class StringFieldDataModel
    {
        [System.Xml.Serialization.XmlAttribute()]
        public string Name { get; set; }
        [System.Xml.Serialization.XmlAttribute()]
        public string Code { get; set; }

        public List<StringFieldRecord> Records { get; set; }
    }

}
