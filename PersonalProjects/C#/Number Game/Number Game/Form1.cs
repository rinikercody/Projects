/* Author: Cody Riniker */
/* This program is a game were the user will try to keep up with the computer's number on screen. */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Number_Game
{
    public partial class Form1 : Form
    {
        //The number the player is controlling
        private int _playerNumber = 0;

        //The oppenent computers number
        private int _computerNumber = 0;

        //Tick event is used to handle progression of computer player.
        private Timer _timer = new Timer();

        //Used to determine if the player has been out of range of the computers number for too long.
        private Stopwatch _playerWatch = new Stopwatch();
        private int _playerTime;

        //Used to get random numbers for computer
        private Random _random = new Random();

        //The allowed difference between player's and computer's number
        private int _distance;

        //Controls the range of numbers the computer will increment/decrement by
        private int _computerRange;

        //Used to help manage speed of computer after the timers intervals get lower.
        private Stopwatch _waitTimer = new Stopwatch();
        private int _waitTime;

        //What the computers speed increase by
        private int _speedIncrease;

        //Tracks players score
        private Stopwatch _scoreClock = new Stopwatch();
        private int _mulitplier;

        public Form1()
        {
            InitializeComponent();
            _timer.Tick += TickEvent;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 'w')
            {
                _playerNumber++;
                uxPlayerLabel.Text = _playerNumber.ToString();
            }
            if(e.KeyChar == 's')
            {
                _playerNumber--;
                uxPlayerLabel.Text = _playerNumber.ToString(); 
            }

            //Start the game
            if(e.KeyChar == (char)Keys.Enter)
            {
                if (uxEasyButton.Checked) //Easy mode
                {
                    _distance = 10;
                    _speedIncrease = 100;
                    _playerTime = 5000;
                    _waitTime = 5000;
                    _computerRange = 5;
                    _mulitplier = 1;
                }
                else if (uxMediumButton.Checked) //Medium mode
                {
                    _distance = 7;
                    _speedIncrease = 200;
                    _playerTime = 3000;
                    _waitTime = 3000;
                    _computerRange = 10;
                    _mulitplier = 3;
                }
                else //Hard mode
                {
                    _distance = 5;
                    _speedIncrease = 300;
                    _playerTime = 2000;
                    _waitTime = 2000;
                    _computerRange = 12;
                    _mulitplier = 5;
                }

                //Setup
                uxEasyButton.Hide();
                uxMediumButton.Hide();
                uxHardButton.Hide();
                this.Text = "Keep Up With The Computer";
                _playerNumber = 0;
                _computerNumber = 0;
                uxPlayerLabel.Text = "0";
                uxComputerLabel.Text = "0";

                //Start Game
                _scoreClock.Start();
                _waitTimer.Start();
                _timer.Start();
            }
        }

        //Executed after each interval of time by _timer
        private void TickEvent(object sender, EventArgs e)
        {
            //Determines if player is out of range of computer
            if(_computerNumber > _playerNumber + _distance || _computerNumber < _playerNumber - _distance)
            {
                _playerWatch.Start(); //Player is out of range and needs to get back in range
            }
            else
            {
                _playerWatch.Reset(); //Player is back in range so timer is reset
            }

            //If player has been out of range for too long, Game is over
            if (_playerWatch.ElapsedMilliseconds >= _playerTime)
            {
                _timer.Stop();
                _playerWatch.Stop();
                MessageBox.Show("You lose\nYour Score Is:\n" + _scoreClock.ElapsedMilliseconds * _mulitplier);
                
                //Reset Game
                _playerNumber = 0;
                _computerNumber = 0;
                uxEasyButton.Show();
                uxMediumButton.Show();
                uxHardButton.Show();
            }

            //Computer Player will now act
            if (_waitTimer.ElapsedMilliseconds >= _waitTime)
            {
                _waitTimer.Reset();

                //Gets a random number to add or substract from the computer's number
                int num = _random.Next(_computerRange);
                int sign = _random.Next(2);
                if (sign == 0) _computerNumber -= num;
                if (sign == 1) _computerNumber += num;
                uxComputerLabel.Text = _computerNumber.ToString();

                if(_waitTime > 700) //Make sure numbers don't change to fast
                {
                    //Computer will now change it's number faster
                    _waitTime -= _speedIncrease;
                }
                _waitTimer.Start();
            }
        }

    } //End class
} //End namepace
