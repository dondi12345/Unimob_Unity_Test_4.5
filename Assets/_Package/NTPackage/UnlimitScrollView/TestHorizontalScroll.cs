using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NTFunctions
{
    public class TestHorizontalScroll : HorizontalScroll
    {
        public List<int> List;
        public int MaxValue = 100;
        protected override void Start()
        {
            this.List = new List<int>();
            for (int i = 0; i < this.MaxValue; i++)
            {
                this.List.Add(i);
            }
            this.OnUI(this.List.Count);
        }
    }
}
