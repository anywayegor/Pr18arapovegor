using System;
using Microsoft.Maui.Controls;

namespace MauiMonsterFight
{
    public partial class MainPage : ContentPage
    {
        const int PlayerMaxHp = 100;
        const int MonsterMaxHp = 80;

        int playerHp;
        int monsterHp;
        Random rnd = new Random();

        public MainPage()
        {
            InitializeComponent();
            ResetGame();
        }

        void ResetGame()
        {
            playerHp = PlayerMaxHp;
            monsterHp = MonsterMaxHp;
            UpdateUi();
            BtnAttack.IsEnabled = false;
            BtnHeal.IsEnabled = false;
            BtnSuper.IsEnabled = false;
            AppendLog("Бой начат...");
        }

        void UpdateUi()
        {
            PlayerHpLabel.Text = $"HP: {playerHp} / {PlayerMaxHp}";
            MonsterHpLabel.Text = $"HP: {monsterHp} / {MonsterMaxHp}";
            PlayerHpBar.Progress = Math.Max(0, (double)playerHp / PlayerMaxHp);
            MonsterHpBar.Progress = Math.Max(0, (double)monsterHp / MonsterMaxHp);
        }

        void AppendLog(string text)
        {
            LogLabel.Text += (LogLabel.Text.Length > 0 ? "\n" : "") + text;
        }

        private void BtnNewGame_Clicked(object sender, EventArgs e)
        {
            ResetGame();
            AppendLog("Новая игра запущена.");
        }

        private void BtnAttack_Clicked(object sender, EventArgs e)
        {
            int dmg = rnd.Next(6, 13);
            monsterHp -= dmg;
            AppendLog($"Игрок атакует и наносит {dmg} урона монстру.");
            if (monsterHp <= 0)
            {
                monsterHp = 0;
                UpdateUi();
                AppendLog("Монстр повержен! Победа!");
                BtnAttack.IsEnabled = BtnHeal.IsEnabled = BtnSuper.IsEnabled = false;
                return;
            }
            UpdateUi();
            MonsterTurn();
        }

        private void BtnHeal_Clicked(object sender, EventArgs e)
        {
            int heal = rnd.Next(8, 15);
            playerHp = Math.Min(PlayerMaxHp, playerHp + heal);
            AppendLog($"Игрок восстанавливает {heal} HP.");
            UpdateUi();
            MonsterTurn();
        }

        private void BtnSuper_Clicked(object sender, EventArgs e)
        {
            int dmg = rnd.Next(12, 21);
            monsterHp -= dmg;
            AppendLog($"Игрок использует Супер-атаку и наносит {dmg} урона!");
            if (monsterHp <= 0)
            {
                monsterHp = 0;
                UpdateUi();
                AppendLog("Монстр повержен! Победа!");
                BtnAttack.IsEnabled = BtnHeal.IsEnabled = BtnSuper.IsEnabled = false;
                return;
            }
            UpdateUi();
            MonsterTurn();
        }

        void MonsterTurn()
        {
            int dmg = rnd.Next(5, 11);
            playerHp -= dmg;
            AppendLog($"Монстр наносит {dmg} урона игроку.");
            if (playerHp <= 0)
            {
                playerHp = 0;
                UpdateUi();
                AppendLog("Игрок повержен. Игра окончена.");
                BtnAttack.IsEnabled = BtnHeal.IsEnabled = BtnSuper.IsEnabled = false;
                return;
            }
            UpdateUi();
        }
    }
}