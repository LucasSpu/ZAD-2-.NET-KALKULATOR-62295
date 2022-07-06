using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NET_INIS4_PR2._2_z2
{
    public class Dane : INotifyPropertyChanged
    {
        bool
            flagaUłamka = false,
            flagaPrzecinka = false,
            flagaWyniku = false,
            flagaUjemnegoZnaku = false
            ;
        double wynik = 0;
        double?
            pierwsza = null,
            druga = null
            ;
        string działanie = null;
        string ostatnieDz = null;
        public string Wynik
        {
            get { 
                if(flagaUjemnegoZnaku && wynik == 0)
                    return "-" + Convert.ToString(wynik);
                else
                    return Convert.ToString(wynik);
            }
            set
            {
                wynik = Convert.ToDouble(value);
                OnPropertyChanged();
            }
        }
        public string OstatnieDz
        {
            get
            {
                return ostatnieDz;
            }
            set
            {

                ostatnieDz = value;
                OnPropertyChanged();
            }
        }

        public void Dopisz(string znak)
        {
            if (flagaWyniku)
                Zeruj();
            if (znak == ".")
                if (flagaUłamka)
                    ;
                else
                    flagaPrzecinka = true;
            else if (flagaPrzecinka)
            {
                Wynik += "." + znak;
                flagaPrzecinka = false;
                flagaUłamka = true;
            }
            else
                Wynik += znak;
        }
        public void ZmieńZnak()
        {
            if (flagaWyniku)
                Zeruj();
            flagaUjemnegoZnaku = !flagaUjemnegoZnaku;
            Wynik = Convert.ToString(-wynik);
        }
        public void Zeruj()
        {
            flagaPrzecinka = flagaPrzecinka = flagaWyniku = flagaUjemnegoZnaku = false;
            druga = null;
            Wynik = "0";
        }
        public void Resetuj()
        {
            Zeruj();
            pierwsza = null;
            działanie = null;
        }
        public void Działanie(string działanie)
        {
            if(pierwsza == null && działanie == "sqrt" || pierwsza == null && działanie == "log(e)" || pierwsza == null && działanie == "1/X" || pierwsza == null && działanie == "Floor" || pierwsza == null && działanie == "Ceiling")
            {
                pierwsza = wynik;
                Wykonaj();
                flagaPrzecinka = false;
                flagaUłamka = false;
                this.działanie = działanie;
            }
            if (pierwsza == null)
            {
                pierwsza = wynik;
                this.działanie = działanie;
                flagaPrzecinka = false;
                flagaUłamka = false;
                Zeruj();
            }
            else 
                {

                
               
                Wykonaj();
                flagaPrzecinka = false;
                flagaUłamka = false;
                druga = wynik;
                this.działanie = działanie;
            }
        }
        public void Wykonaj()
        {
            if (działanie == null)
                return;
            else if (druga == null || druga == 0)
                druga = wynik;

            if (działanie == "+")
                Wynik = Convert.ToString(pierwsza + druga);
            else if (działanie == "-")
                Wynik = Convert.ToString(pierwsza - druga);
            else if (działanie == "*")
                Wynik = Convert.ToString(pierwsza * druga);
            else if (działanie == "/")
                Wynik = Convert.ToString(pierwsza / druga);
            else if (działanie == "1/X")
                Wynik = Convert.ToString(1.0 / (double)pierwsza);
            else if (działanie == "modulo")
                Wynik = Convert.ToString(pierwsza % druga);
            else if (działanie == "^")
                Wynik = Convert.ToString(Math.Pow((double)pierwsza, (double)druga));
            else if (działanie == "%")
                Wynik = Convert.ToString(pierwsza * (druga / 100));
            else if (działanie == "sqrt")
                Wynik = Convert.ToString(Math.Sqrt((double)pierwsza));
            else if (działanie == "log(e)")
                Wynik = Convert.ToString(Math.Log((double)pierwsza));
            else if (działanie == "Floor")
                Wynik = Convert.ToString(Math.Floor((double)pierwsza));
            else if (działanie == "Ceiling")
                Wynik = Convert.ToString(Math.Ceiling((double)pierwsza));

            if (działanie == "+" || działanie == "-" || działanie == "*" || działanie == "/" || działanie == "modulo" || działanie == "%" || działanie == "^")
            {  OstatnieDz = pierwsza + działanie + druga + "=" + wynik; }
            else if (działanie == "1/X" || działanie == "sqrt" || działanie == "log(e)" || działanie == "Floor" || działanie == "Ceiling")
            { OstatnieDz = działanie +"("+ pierwsza +")"+ "=" + wynik; }

            flagaPrzecinka = false;
            flagaUłamka = false;
            flagaWyniku = true;
            pierwsza = wynik;
            druga = null;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
