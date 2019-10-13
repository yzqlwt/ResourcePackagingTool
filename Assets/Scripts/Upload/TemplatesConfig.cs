using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Scripts.Upload
{
    class Target
    {
        public int id;
        public string name;
        public string game_id;
    }
    class TemplatesConfig
    {
        public int start;
        public int count;
        public int total;
        public Target[] targets;
    }
}
