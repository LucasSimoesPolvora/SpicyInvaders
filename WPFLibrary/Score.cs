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
        public double boost = 1;

        /// <summary>
        /// Permet de update le score
        /// </summary>
        /// <returns></returns>
        public void update()
        {
             ScoreValue += MaxDeadValue;
        }

        /// <summary>
        /// Ecris le score correspondant
        /// </summary>
        /// <returns>Le string qui contient le score</returns>
        public string writeScore()
        {
            return $"Score : {ScoreValue}";
        }

        /// <summary>
        /// Enelve de la valeur aux ennemis lorsqu'on rate une balle
        /// </summary>
        public void downADeadValue()
        {
            if(MaxDeadValue >= 10)
            {
                MaxDeadValue -= 1;
            }
            else
            {

            }
        }

        /// <summary>
        /// Rajoute du boost aux ennemis après chaque score
        /// </summary>
        public void moreBoost()
        {
                boost += 0.1;   
        }
    }
}
