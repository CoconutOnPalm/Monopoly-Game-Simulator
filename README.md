# Monopoly Game Simulator
> Lost a game of Monopoly to my buddies once, so I created this thing to ensure I never lose again.
> 
> ~CoconutOnPalm

I have lost more games since then (and will certainly lose even more), but this simulator showed me that skill is only a part of victory - luck truly matters

**Monopoly Game Simulator** allows you to simulate one or multiple games of *modified* Monopoly game. The modification itself was created by me and my friends to counter the boredom and stability of the normal game

## Modified rules
Every property belongs to the **God Bless You Bank** (pol: Bank Bóg Zapłać). The difference between this bank and the normal one is that **God Bless You Bank** can lend you money in some specific cases:
1. Player **can** take a loan when:
   - He/She stands on someone else's property
2. Player **can NOT** take a loan when:
   - He/She wants to buy a property
   - He/She wants to buy a house/hotel
   - He/She stands on a penalty tile ("Pay 200" tile or "Pay 100" tile)
   - He/She stands on a police tile and wants to escape it

However, the debt must not exceed the certain amount (e.g.: 5000). When a player does not have enough money to pay it off, he/she looses. Debt can only be payed with cash, as it is not a Casino-Royale style gameplay
There are additional rules that control the flow of the game, as well as some other important aspects:
- Additional rules:
  - at one point of the game, all players exchange their cards with each other so all tiles are grouped in streets
    - for example: Players exchange their cards, so in the end: *Player 1* has **orange** and **green** streets, *Player 2* has **red** and **yellow** streets and as a resoult every player has streets and not random tiles scattered across the board
  - every player must give donations to the Church, or else he/she will be seen as a weak one
  - all donations are deposited to the parking tile
  - do not give the Bank to the most sus friend, as he/she will most certainly squander the church's wealth
 
## How to use (example)
In this example we will create a game with 3 players:
- Player1: [Name]: Gniewomir, [Money on start]: 300, [Debt on start]: 210

- Player2: [Name]: Bożydar,   [Money on start]: 100, [Debt on start]: 370
- Player3: [Name]: Męcichuj,  [Money on start]: 200, [Debt on start]: 420

Each one of them will have:
- Player1: Yellow and Red tiles
- Player2: Green and Orange tiles
- Player3: Blue, Magenta nad Cyan tiles

### Step 1: set up players
Now, go to the *Manage players* box in the upper right corner. Rename Player1 and Player2 and set their money and debt values. Then, check the *include* box on the third line. This will add Player3 to the game. Cufigure Player3 just as you did with Player1 and Player2. 

![example1](https://github.com/CoconutOnPalm/Monopoly-Game-Simulator/assets/62422875/748df19a-3594-42f9-8984-e3b8740840e6)

### Step 2: give them properites
Now, select **[1]** box near the Gniewomir's name - this will select him. On the left side of the window there is a huge box of different tiles. Select with mouse all Yellow and Red tiles (tip: you can use "Select whole tile" button to automtically select all tiles in the group). Then, select Bożydar ([2] box near his name) select Green and Orange tiles. Repeat this process for Męcichuj. 

![example2](https://github.com/CoconutOnPalm/Monopoly-Game-Simulator/assets/62422875/f6c3b2df-921e-4b88-9e4d-cf73284ab5a0)

See how selected tiles are highlighted. When you move to pick tiles for other player, these tiles will appear faded
You can also change property settings in *Manage player's properties* box

### Step 3: run the simulation and see the resoults
When you are ready, you can run the simulation simply by clicking the **Simulate** button. Before that, you can change few variables as well as sim mode. There are two awailable modes:
1. ***single game*** mode
   - simulates only one game, but provides additional data in the chart window
   - shows which player won and what was his/her balance in the end
   - the output data is shown in the *Singlegame mode* tab in *Simulation Output* box
2. ***multiple games*** mode
   - simulates multiple games (set in *Simulated Games* control)
   - shows in how many % of games each player won/survived/lost
   - the output data is shown in the *Multigame mode* tab in *Simulation Output* box

For more data you can open the chart window, where you can see detailed data about the simulation (Pressing ctrl+S will save the image of the window)

**SELECT *SINGLE GAME* MODE**

![example3](https://github.com/CoconutOnPalm/Monopoly-Game-Simulator/assets/62422875/61539a0f-cfca-4ba1-97f3-8d331ba1c523)

in this example: Gniewomir won, Bożydar survived (did not lose the game, but ended with debt) and Męcichuj lost. The whole game took only 19 turns.
Now, open the chart window

![example4](https://github.com/CoconutOnPalm/Monopoly-Game-Simulator/assets/62422875/b5de773e-f8bb-414e-b4b9-fe869a943773)

The first chart shows tile data: 
1. Colums mark each tile's profit:
   - in this example: *Most Grunwaldzki* made 3450 for it's owner, and *Politechnika Wrocławska* 2800. The exact names are how as a tooltip (you have to hower the mouse over the column)
2. The line tells us how many times someone stopped at this tile
   - in this example: 2 people stopped at *Uniwersytet Medyczny*, and *Grape Hotel* had the most traffic (4 times)
  
The next two graphs show the balance of each player in every turn (where Y is the balance, ad X are the tunrns) 

**SELECT *MILTI GAME* MODE**

![example5](https://github.com/CoconutOnPalm/Monopoly-Game-Simulator/assets/62422875/b17bfca8-0d59-4849-8ee9-f495b6e884fa)

As we learn from the output, with out of 100 games
* Gniewomir won 21% of them, in 50% he ended game with debt, and lost 29%
* Same for Bożyar: [29%, 38%, 33%]
* and Męcichuj: [21%, 41%, 38%]

## Final conclusion
It all depends on luck, or being more specific: how much you donate to the Church

Don't ask me why I made this. I am still trying answer it myself.

# Cloning the project
The projest is made with .NET framework 4.8 and WinForms. After you clone the repository, copy the `res` folder with everything inside to the `bin\Debug\` or `bin\Release\` folders, because .net framework has problems with itself and I don't like the idea of resolving them (unlike c++, the default directory is not in the solution dir). If there is any way to tweak it in a *simple* way I am opened for any ideas.
