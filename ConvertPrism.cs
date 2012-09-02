using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class ConvertPrism
    {
        //Ruffus
        //GG: 1283452
        public string Convert(string toDouble)
        {
            //Nieruszane w stosunku do wersji 0

            string kwota = null;
            string WorkingString = null;

            string wogroszy = toDouble.Substring(0, toDouble.IndexOf(',', 0));
            string grosze = "";
            grosze += toDouble[toDouble.IndexOf(',') + 1];
            grosze += toDouble[toDouble.IndexOf(',') + 2];

            string post = "milionów";

            if (wogroszy.Length < 9)
            {
                for (int i = 0; i < 9 - wogroszy.Length; i++)
                    WorkingString += "0";
            }
            WorkingString += wogroszy;

            char[,] triples = { { WorkingString[0], WorkingString[1], WorkingString[2] }, { WorkingString[3], WorkingString[4], WorkingString[5] }, { WorkingString[6], WorkingString[7], WorkingString[8] } };

            char[] gr = { grosze[1], grosze[0] };

            #region MILION
            //Ileset milionów?
            if (triples[0, 0] == '1')
                kwota += " sto";
            else if (triples[0, 0] == '2')
                kwota += " dwieście";
            else if (triples[0, 0] == '3')
                kwota += " trzysta";
            else if (triples[0, 0] == '4')
                kwota += " czterysta";
            else if (triples[0, 0] == '5')
                kwota += " pięćset";
            else if (triples[0, 0] == '6')
                kwota += " sześćset";
            else if (triples[0, 0] == '7')
                kwota += " siedemset";
            else if (triples[0, 0] == '8')
                kwota += " osiemset";
            else if (triples[0, 0] == '9')
                kwota += " dziewięćset";
            //Iledziesiąt tysięcy?
            if (triples[0, 1] == '2')
                kwota += " dwadzieścia";
            else if (triples[0, 1] == '3')
                kwota += " trzydzieści";
            else if (triples[0, 1] == '4')
                kwota += " czterdzieści";
            else if (triples[0, 1] == '5')
                kwota += " pięćdziesiąt";
            else if (triples[0, 1] == '6')
                kwota += " sześćdziesiąt";
            else if (triples[0, 1] == '7')
                kwota += " siedemdziesiąt";
            else if (triples[0, 1] == '8')
                kwota += " osiemdziesiąt";
            else if (triples[0, 1] == '9')
                kwota += " dziewięćdziesiąt";

            if ((triples[0, 1] == '1') && (triples[0, 2] == '0'))
                kwota += " dziesięć";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '1'))
                kwota += " jedenaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '2'))
                kwota += " dwanaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '3'))
                kwota += " trzynaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '4'))
                kwota += " czternaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '5'))
                kwota += " piętnaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '6'))
                kwota += " szesnaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '7'))
                kwota += " siedemnaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '8'))
                kwota += " osiemnaście";
            else if ((triples[0, 1] == '1') && (triples[0, 2] == '9'))
                kwota += " dziewiętnaście";

            else if (triples[0, 2] == '1')
            {
                kwota += " jeden";
                if (kwota == " jeden")
                    post = "milion";
            }
            else if (triples[0, 2] == '2')
            {
                kwota += " dwa";
                post = "miliony";
            }
            else if (triples[0, 2] == '3')
            {
                kwota += " trzy";
                post = "miliony";
            }
            else if (triples[0, 2] == '4')
            {
                kwota += " cztery";
                post = "miliony";
            }
            else if (triples[0, 2] == '5')
                kwota += " pięć";
            else if (triples[0, 2] == '6')
                kwota += " sześć";
            else if (triples[0, 2] == '7')
                kwota += " siedem";
            else if (triples[0, 2] == '8')
                kwota += " osiem";
            else if (triples[0, 2] == '9')
                kwota += " dziewięć";
            //Czy dodawać sufix:
            if (wogroszy.Length > 6)
                kwota += " " + post;
            #endregion

            #region HUNNTAUSEND
            post = "tysięcy";
            //Ileset tysięcy?
            if (triples[1, 0] == '1')
                kwota += " sto";
            else if (triples[1, 0] == '2')
                kwota += " dwieście";
            else if (triples[1, 0] == '3')
                kwota += " trzysta";
            else if (triples[1, 0] == '4')
                kwota += " czterysta";
            else if (triples[1, 0] == '5')
                kwota += " pięćset";
            else if (triples[1, 0] == '6')
                kwota += " sześćset";
            else if (triples[1, 0] == '7')
                kwota += " siedemset";
            else if (triples[1, 0] == '8')
                kwota += " osiemset";
            else if (triples[1, 0] == '9')
                kwota += " dziewięćset";
            //Iledziesiąt tysięcy?
            if (triples[1, 1] == '2')
                kwota += " dwadzieścia";
            else if (triples[1, 1] == '3')
                kwota += " trzydzieści";
            else if (triples[1, 1] == '4')
                kwota += " czterdzieści";
            else if (triples[1, 1] == '5')
                kwota += " pięćdziesiąt";
            else if (triples[1, 1] == '6')
                kwota += " sześćdziesiąt";
            else if (triples[1, 1] == '7')
                kwota += " siedemdziesiąt";
            else if (triples[1, 1] == '8')
                kwota += " osiemdziesiąt";
            else if (triples[1, 1] == '9')
                kwota += " dziewięćdziesiąt";

            if ((triples[1, 1] == '1') && (triples[1, 2] == '0'))
                kwota += " dziesięć";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '1'))
                kwota += " jedenaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '2'))
                kwota += " dwanaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '3'))
                kwota += " trzynaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '4'))
                kwota += " czternaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '5'))
                kwota += " piętnaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '6'))
                kwota += " szesnaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '7'))
                kwota += " siedemnaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '8'))
                kwota += " osiemnaście";
            else if ((triples[1, 1] == '1') && (triples[1, 2] == '9'))
                kwota += " dziewiętnaście";

            else if (triples[1, 2] == '1')
            {
                kwota += " jeden";
                if (kwota == " jeden")
                    post = "tysiąc";
            }
            else if (triples[1, 2] == '2')
            {
                kwota += " dwa";
                post = "tysiące";
            }
            else if (triples[1, 2] == '3')
            {
                kwota += " trzy";
                post = "tysiące";
            }
            else if (triples[1, 2] == '4')
            {
                kwota += " cztery";
                post = "tysiące";
            }
            else if (triples[1, 2] == '5')
                kwota += " pięć";
            else if (triples[1, 2] == '6')
                kwota += " sześć";
            else if (triples[1, 2] == '7')
                kwota += " siedem";
            else if (triples[1, 2] == '8')
                kwota += " osiem";
            else if (triples[1, 2] == '9')
                kwota += " dziewięć";
            //Czy dodawać sufix:
            if (wogroszy.Length > 3)
                kwota += " " + post;
            #endregion

            #region HUNDREADS
            post = "";
            //Ileset tysięcy?
            if (triples[2, 0] == '1')
                kwota += " sto";
            else if (triples[2, 0] == '2')
                kwota += " dwieście";
            else if (triples[2, 0] == '3')
                kwota += " trzysta";
            else if (triples[2, 0] == '4')
                kwota += " czterysta";
            else if (triples[2, 0] == '5')
                kwota += " pięćset";
            else if (triples[2, 0] == '6')
                kwota += " sześćset";
            else if (triples[2, 0] == '7')
                kwota += " siedemset";
            else if (triples[2, 0] == '8')
                kwota += " osiemset";
            else if (triples[2, 0] == '9')
                kwota += " dziewięćset";
            //Iledziesiąt tysięcy?
            if (triples[2, 1] == '2')
                kwota += " dwadzieścia";
            else if (triples[2, 1] == '3')
                kwota += " trzydzieści";
            else if (triples[2, 1] == '4')
                kwota += " czterdzieści";
            else if (triples[2, 1] == '5')
                kwota += " pięćdziesiąt";
            else if (triples[2, 1] == '6')
                kwota += " sześćdziesiąt";
            else if (triples[2, 1] == '7')
                kwota += " siedemdziesiąt";
            else if (triples[2, 1] == '8')
                kwota += " osiemdziesiąt";
            else if (triples[2, 1] == '9')
                kwota += " dziewięćdziesiąt";

            if ((triples[2, 1] == '1') && (triples[2, 2] == '0'))
                kwota += " dziesięć";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '1'))
                kwota += " jedenaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '2'))
                kwota += " dwanaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '3'))
                kwota += " trzynaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '4'))
                kwota += " czternaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '5'))
                kwota += " piętnaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '6'))
                kwota += " szesnaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '7'))
                kwota += " siedemnaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '8'))
                kwota += " osiemnaście";
            else if ((triples[2, 1] == '1') && (triples[2, 2] == '9'))
                kwota += " dziewiętnaście";

            else if (triples[2, 2] == '1')
            {
                kwota += " jeden";
            }
            else if (triples[2, 2] == '2')
            {
                kwota += " dwa";
            }
            else if (triples[2, 2] == '3')
            {
                kwota += " trzy";
            }
            else if (triples[2, 2] == '4')
            {
                kwota += " cztery";
            }
            else if (triples[2, 2] == '5')
                kwota += " pięć";
            else if (triples[2, 2] == '6')
                kwota += " sześć";
            else if (triples[2, 2] == '7')
                kwota += " siedem";
            else if (triples[2, 2] == '8')
                kwota += " osiem";
            else if (triples[2, 2] == '9')
                kwota += " dziewięć";
            //Czy dodawać sufix:
            post = "zł ,";
            if (wogroszy.Length > 0)
                kwota += " " + post;
            #endregion

            #region GROSZE
            //Iledziesiąt groszy?
            if (grosze[0] == '2')
                kwota += " dwadzieścia";
            else if (grosze[0] == '3')
                kwota += " trzydzieści";
            else if (grosze[0] == '4')
                kwota += " czterdzieści";
            else if (grosze[0] == '5')
                kwota += " pięćdziesiąt";
            else if (grosze[0] == '6')
                kwota += " sześćdziesiąt";
            else if (grosze[0] == '7')
                kwota += " siedemdziesiąt";
            else if (grosze[0] == '8')
                kwota += " osiemdziesiąt";
            else if (grosze[0] == '9')
                kwota += " dziewięćdziesiąt";

            if ((grosze[0] == '1') && (grosze[1] == '0'))
                kwota += " dziesięć";
            else if ((grosze[0] == '1') && (grosze[1] == '1'))
                kwota += " jedenaście";
            else if ((grosze[0] == '1') && (grosze[1] == '2'))
                kwota += " dwanaście";
            else if ((grosze[0] == '1') && (grosze[1] == '3'))
                kwota += " trzynaście";
            else if ((grosze[0] == '1') && (grosze[1] == '4'))
                kwota += " czternaście";
            else if ((grosze[0] == '1') && (grosze[1] == '5'))
                kwota += " piętnaście";
            else if ((grosze[0] == '1') && (grosze[1] == '6'))
                kwota += " szesnaście";
            else if ((grosze[0] == '1') && (grosze[1] == '7'))
                kwota += " siedemnaście";
            else if ((grosze[0] == '1') && (grosze[1] == '8'))
                kwota += " osiemnaście";
            else if ((grosze[0] == '1') && (grosze[1] == '9'))
                kwota += " dziewiętnaście";

            else if (grosze[1] == '1')
            {
                kwota += " jeden";
            }
            else if (grosze[1] == '2')
            {
                kwota += " dwa";
            }
            else if (grosze[1] == '3')
            {
                kwota += " trzy";
            }
            else if (grosze[1] == '4')
            {
                kwota += " cztery";
            }
            else if (grosze[1] == '5')
                kwota += " pięć";
            else if (grosze[1] == '6')
                kwota += " sześć";
            else if (grosze[1] == '7')
                kwota += " siedem";
            else if (grosze[1] == '8')
                kwota += " osiem";
            else if (grosze[1] == '9')
                kwota += " dziewięć";
            //Czy dodawać sufix: 
            post = "gr";
            kwota += " " + post;
            #endregion

            return kwota;
        }
    }
}
