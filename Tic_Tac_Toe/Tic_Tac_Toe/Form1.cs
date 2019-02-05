using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


//TO-DO::Persistent problem: Whenever the reset button is pressed, the dialogue box pops-up up twice.


namespace Tic_Tac_Toe
{
    public partial class Form1 : Form
    {
        enum PlayerTurn { None, TurnP1, TurnP2 };
        enum Winner { None, P1, P2, Draw };

        PlayerTurn turn;
        Winner winner;

        void newGame()
        {
            //this is used to make resetting the game easier
            PictureBox[] AllPictures =
            {
                pictureBox0,
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4,
                pictureBox5,
                pictureBox6,
                pictureBox7,
                pictureBox8,
            };

            //clear all the cells.
            foreach (var p in AllPictures)
            {
                p.Image = null;
            }

            turn = PlayerTurn.TurnP1;
            winner = Winner.None;
            Show_Status();
        }

        Winner Get_the_Winner()
        {
            //all the winning moves
            PictureBox[] winning_moves =
            {   //we will be checking each possible position for a winner, in case that we find one, 
                //the game will stop and the winning player will get a point.
                pictureBox0,pictureBox1,pictureBox2,
                pictureBox3,pictureBox4,pictureBox5,
                pictureBox6,pictureBox7,pictureBox8,

                pictureBox0,pictureBox3,pictureBox6,
                pictureBox1,pictureBox4,pictureBox7,
                pictureBox2,pictureBox5,pictureBox8,

                pictureBox0,pictureBox4,pictureBox8,
                pictureBox2,pictureBox4,pictureBox6
            };
            //check the winner
            for (int i = 0; i < winning_moves.Length; i += 3)
            {
                if (winning_moves[i].Image != null)
                {
                    if (winning_moves[i].Image == winning_moves[i + 1].Image && winning_moves[i].Image == winning_moves[i + 2].Image)
                        //we got ya
                        if (winning_moves[i].Image == Player_1.Image)
                            return Winner.P1;
                        else
                            return Winner.P2;
                }
            }

            //Checking for empty cells.
            PictureBox[] AllPictures =
            {
                pictureBox0,
                pictureBox1,
                pictureBox2,
                pictureBox3,
                pictureBox4,
                pictureBox5,
                pictureBox6,
                pictureBox7,
                pictureBox8,
            };

            foreach (var p in AllPictures)
            {
                if (p.Image == null)
                    return Winner.None;
            }
            //If this point is reached, it is a draw between the 2 players.
            return Winner.Draw;
        }

        void Show_Status()
        {
            //status is for the label of the players.
            //message is the pop-up message whenever there's a winner or there's a draw.
            string status = "";
            string message = "";
            switch (winner)
            {
                case Winner.None:
                    if (turn == PlayerTurn.TurnP1)
                        status = "Player_1";
                    else
                        status = "Player_2";
                    break;
                case Winner.P1:
                    message = status = "Player_1_Has_Won";
                    break;
                case Winner.P2:
                    message =status = "Player_2_Has_Won";
                    break;
                case Winner.Draw:
                    message = status = "Everyone lost. Happens :(";
                    break;
            }

            lblSts.Text = status;

            if(message != "")
            {
                MessageBox.Show(message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            newGame();
        }

        private void Click_Event(object sender, EventArgs e)
        {
            PictureBox x = sender as PictureBox;

            //Will check who's picture is supposed to be placed at the time of the event.
            if (x.Image != null) 
                return;
           
            if(turn == PlayerTurn.None)           
                return;

            if (turn == PlayerTurn.TurnP1)
                x.Image = Player_1.Image;
            else
                x.Image = Player_2.Image;

            //Check if winner
            winner = Get_the_Winner();

            if (winner == Winner.None)
            {
                //Chaning the turns.
                turn = (PlayerTurn.TurnP1 == turn) ? PlayerTurn.TurnP2 : PlayerTurn.TurnP1;
            }
            else
                turn = PlayerTurn.None;

            Show_Status();

        }

        private void Reset_Game(object sender, EventArgs e)
        {
            var check = MessageBox.Show("Are you sure you want to reset?",
                                        "New Game",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question
                                        );
            if(check == DialogResult.Yes)
                newGame();   
        }

        private void Exit(object sender, FormClosingEventArgs e)
        {
            var check = MessageBox.Show("Are you sure you want to exit?",
                                        "Exiting application",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information
                                        );
            if (check == DialogResult.No)
                e.Cancel = true;
        }

        private void Player_1_Click(object sender, EventArgs e)
        {

        }
    }
}
