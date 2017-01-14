using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksPlayer
{
    class Field
    {
        public Boolean isUpperFree { get; set; }
        public Boolean isBottomFree { get; set; }
        public Boolean isLeftFree { get; set; }
        public Boolean isRightFree { get; set; }
        public Boolean isOccupied { get; set; }

        public Field()
        {
            this.isUpperFree = true;
            this.isBottomFree = true;
            this.isLeftFree = true;
            this.isRightFree = true;
            this.isOccupied = false;
        }

        public void OccupyField()
        {
            this.isOccupied = true;
        }

        public Boolean isBlockable()
        {
            if (this.isOccupied)
                return false;
            int k = 0;
            if (this.isBottomFree)
                k++;
            if (this.isLeftFree)
                k++;
            if (this.isRightFree)
                k++;
            if (this.isUpperFree)
                k++;

            if(k == 0)
            {
                this.isOccupied = true;
                
            }

            if (k == 1)
                return true;
            else
                return false; 
        }

        public String ToString()
        {
            return " " + this.isOccupied + " " + this.isUpperFree  + " " + this.isBottomFree + " " + this.isLeftFree + " " + this.isRightFree;

        }

    }
}
