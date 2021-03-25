# Pomodoro
Visual Programming Project Task

Pomodoro Application
# 1.	Description

Pomodoro is an application whose name is based on the name of the technique with the same name that should enable the most efficient use of time. The rules used by the technique are the same ones that this application requires, and they are the following:

•	Find a to-do list that you want to complete during the day

•	Choose one of them

•	Work on it until the timer expires

•	If the task you worked on is completed, mark it as done

•	Rest when you go through a Pomodoro cycle

•	Repeat the process as many times as needed

## 1.1.      Import project

Import `Pomodoro.sln` in [Visual Studio](https://visualstudio.microsoft.com/) and start the project.

## 1.2.	 How to use the application
![Screenshot](Pictures/p1.png)
![Screenshot](Pictures/p2.png)
![Screenshot](Pictures/p4.png)
![Screenshot](Pictures/p5.png)

# 2.	Description of the solution

## •	Circular Progress Bar
Circular Progress Bar is a custom user control which means that it can be set by drag-and-drop from the Toolbox. It contains the progress (for the timer) which is used in both methods for drawing and updating it. The way it is used is through Threads which are manipulated by starting and pausing them. 
## •	Timer
The `Timer` class contains a Progress Bar (`pbTime`), Timer (`timer1`), several flags (`wasPaused, flag, nightMode`), `menuStrip` and several lists. Mainly in this class we deal with events by clicking buttons, changing colors and ticking off the timer. 

## •	Clock class
The `Clock` class is an auxiliary class in which we keep minutes and seconds with which we simulate the operation of a digital clock. This class is used to manipulate the time spent in the `Timer` class. 

## •	Distraction
It is used to add tasks that appear during the work with the application, where the tasks are ordinary strings that are added to the CheckBox List. 

## •	ToDo
Is a form where the tasks to be performed during a Pomodoro cycle are stored. A list is kept of all the tasks we have set and completed. There is an option to add and delete tasks. 

## •	Pauza
The `Pauza` form appears after completing a `25` minute cycle. There is an option to start the pause or turn off the form which would mean canceling the pause. Additionally, the duration of the break is determined by the number of pomodoro cycles completed.

# 3.	Description of certain functions and classes of the source code
```c#
private void btnStartStop_Click(object sender, EventArgs e) {
            flag = !flag;
            if(flag) {
                btnStartStop.Text = "Pause";
                timer1.Start();
            }
            else {
                btnStartStop.Text = "Start";
                timer1.Stop();
            }

            if(clock.actualSec == 0 && clock.minutes == 0) {
                clock.restart();
                label2.Text = clock.ToString();
            }

            Task.Factory.StartNew(() => {

                 for(int num = progress; num <= 100; num++) {

                     if(!flag) {
                         wasPaused = true;
                     }
                     else {
                         new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.ProgressUpgrade)).Start(num);

                         if(wasPaused) {
                             System.Threading.Thread.Sleep(tempCounter);
                             wasPaused = false;
                         }
                         else {
                             System.Threading.Thread.Sleep(counter);
                         }

                     }
                 }

             });

            progress = pbTime.progress;
        }
```
The corresponding code is an event handler that manages the timer and the progress bar. It stores a flag that checks whether the timer should be paused or not, just like the progress bar. The text of the button is also updated based on the flag. In this section we also deal with pausing and restarting the timer, in the sense where the progress bar needs to be paused or resumed. The flag `wasPaused` and the auxiliary variable `tempCounter` are used for this purpose, which indicates how much longer the Thread should sleep. 

```c#
class Clock {
        public int minutes { get; set; }
        public int actualSec { get; set; }
        public int showSec { get; set; }
        public bool firstRound;
        public int pomoshniMin;

        public Clock() {
            minutes = 25;
            showSec = 0;
            actualSec = 60;
            firstRound = true;
        }

        public Clock(int minutes) {
            this.minutes = minutes;
            showSec = 0;
            actualSec = 60;
            pomoshniMin = minutes;
            firstRound = true;
        }

        public void clockTick() {
            if(firstRound) {
                minutes = minutes-1;
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
            minutes = pomoshniMin;
            showSec = 0;
            actualSec = 60;
            firstRound = true;
        }

        public override string ToString() {
            return string.Format("{0:00} : {1:00}", minutes, showSec);
        }
    }    
```


The given code is a representation of the `Clock` class. It has functions that manipulate the operation of the timer that should work with one Pomodoro cycle, so the minutes are predefined to a value of `25`. The `checkTransfer` function is used to check the need to change the minutes, and if it exists, then the `changeMinute` function is called which is responsible for the action to be taken. After the timer expires, it needs to be restarted, for which the `restart` function is responsible.





## Made by:
### Marija Trajkova
### Aleksandar Stojanovikj
