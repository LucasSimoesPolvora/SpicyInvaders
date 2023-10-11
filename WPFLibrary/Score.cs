using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class score
    {
        public int ScoreValue = 0;
        public int MaxDeadValue = 40;

        /// <summary>
        /// Permet de update le score
        /// </summary>
        /// <returns></returns>
        public void update()
        {
            ScoreValue += MaxDeadValue;
        }

        public string writeScore()
        {
            return $" {ScoreValue}";
        }

        public void downADeadValue()
        {
            if(MaxDeadValue <= 10)
            {

            }
            else
            {
                MaxDeadValue--;
            }
        }
    }
}
