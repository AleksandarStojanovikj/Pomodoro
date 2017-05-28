using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pomodoro {
    [Serializable]
    class Clock {
        public int minutes { get; set; }
        public int actualSec { get; set; }
        public int showSec { get; set; }
        public bool firstRound;

        public Clock() {
            minutes = 25;
            showSec = 0;
            actualSec = 60;
            firstRound = true;
        }

        public void clockTick() {
            if(firstRound) {
                minutes = 24;
                firstRound = false;
            }
            checkTransfer();
            showSec = actualSec;
        }

        public void checkTransfer() {
            if(actualSec == 0) {
                changeMinute();
                actualSec = 59;
            }
            else
                actualSec--;
        }

        public void changeMinute() {
            if(minutes > 0)
                minutes--;
            else {
                restart();
            }
        }

        public void restart() {
            minutes = 25;
            showSec = 0;
            actualSec = 60;
            firstRound = true;
        }



        public override string ToString() {
            return string.Format("{0:00} : {1:00}", minutes, showSec);
        }
    }
}
