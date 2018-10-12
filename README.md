# 3Dcity

THINGS TO Check
Add in feature to reset misses when 20,000 "free man" stage - use CK logic for this
Update resume screen so can shoot bullets and kill enemies but you don't accumulate points
Update grid so yes or no and yes always goes at grid speed
Update code so that once quit and resume DON'T go to ready - maybe change back...
[because currently goes to Play next and spawn all enemies]

Co-ordinate system for touching mid screen text to choose
[instead of left / right / fire]

TEST	12/10/2018
Release + full screen - ensure all vertical correct
Test on Xbox controller - finalize inputs
Test game over can hit fire - do I want to update fire icon?  I should for consistency!!

EASY
220,190
320,190

YES NO
240-340
440-540

Level
200-300
370-480

DONE
When hold down arrow key and game over - the small target does not reset to center
When instant death the miss count increments - need to refactor this for invincibility


11/10/2018
Enemy delay algorithms:

01. NONE
frameDelay - Random(0, frameDelta) 

02. WAVE
frameDelay +/- Sine(random(0,360)) * frameRange
1000 +/- [-1 to 1] * 200 => [800 - 1200]
check (frameDelay < frameMinim [500]) { frameDelay = frameMinim }

03.
frameDelay - "formula"
formula = %level [10-100%] => [0.1-1.0] + Abs(Sine(random(0,360)))) [0-1]
i.e. range [0.1 - 2.0] * frameRange [200]
frameCalcd - [2-400]
frameDelay = 1000 - 400 => [998 - 600]


10/10/2018
Polish...!
01.DONE
Check BulletCollideEnemy()
for Hard and frame 6 or 7
should now be encapsulated in the offset so remove that code
if (enemyFrame >= enemyFrameOffsets.Length - 2)

02.
Check score avoid and score kills values to ensure reconcile to total!

03.
For select screens I'd like to make joy pad collision smaller
OR take precedence for tapping the letters left / right first

04.DONE
Enemy none / wave / fast
Maybe have a better validation algorithm if totals > 100%

05.
Decide on sound Title vs. GameTitle for music


polish code
(Int32)CurrScreen;	(Int32) CurrScreen;
08/10/2018
Movement tolerance

Mobile
Vector2 position = movePositions[index];
Single temp = func(position);
if (Math.Abs(temp) > 0.4f)//Single.Epsilon)
{
}

Desktop
horz = controlManager.CheckJoyPadHorz(mouseScreenInput.MousePosition);
if (Math.Abs(horz) > 0.4f)//Single.Epsilon)
{
}

07/10/2018
Enemy slot 5,6,7 GetEnemyBounds and reduce 72 to 52 to not 
overlap with status bar and text and the bottom of screen

03/10/2018
Prototyping the progress bars
100x20 is not wide enough... should be 200px wide!
i.e. 20x10
Get the value as percentage and multiply by 2x
this is the width of the yellow / red inner rectangle

Also, 20px not high enough should be higher
in fact believe that can go to 24px [max] in sprite sheet 02 [at bottom]
and will "sit" nicely between the bottom of the screen and the grid line


02/10/2018
search for adriana and stevepro

minX
7	200	120	-28	0	8	284		
6	200	120	-28	12	8	272		
5	200	120	-28	20	8	264		
4	200	120	-28	28	8	256		
3	200	120	-28	35	8	249		
2	200	120	-28	40	8	244		
1	200	120	-28	44	8	240		
0	200	120	-28	46	8	238		
enemyFrame	enemyPosX	enemySize	-bulletOffset	-enemyFrameOffset	bulletSize	EASY		
								
maxX								
7	200	120	-28	0	8	284	4	280
6	200	120	-28	12	8	272	2	270
5	200	120	-28	20	8	264	0	264
4	200	120	-28	28	8	256	0	256
3	200	120	-28	35	8	249	0	249
2	200	120	-28	40	8	244	0	244
1	200	120	-28	44	8	240	0	240
0	200	120	-28	46	8	238	0	238
enemyFrame	enemyPosX	enemySize	-bulletOffset	-enemyFrameOffset	bulletSize	EASY		HARD


minX = enemyPosX 	-bulletOffset	-enemyFrameOffset
172  = 200		-28		-0
184  = 200		-28		-12


Enemy
200,200

FR:7	MIDX=228
EASY
TL	172,172		-28,-28
TR	284,172		+84,-28		EX+120-28-8	EY-28		8=width bullet
BL	172,284
BR	284,284

HARD
same as above but 4 deflate
TL	176,176
TR	280,176
BL	176,280
BR	280,280		


176 = 172 + 4
280 = 284 - 4


FR:6
EASY
200,200
inflate		12
200-28+12
200+120-28-8-12=272

TL	184,184		184=200-28+12
TR	272,184
BL	184,272
BR	272,272

HARD
same as above but 2 deflate
TL	186,186		184=200-28+12+2
TR	270,186
BL	186,270
BR	270,270


FR:5
EASY
200,200
inflate		20
200-28+20	192
200+120-28-8-20	264

TL	192,192		
TR	264,192
BL	192,264
BR	264,264

2x values
min
max

200+120-28-8-


TODO
LevelManager

Bullet
MaxBullets
FrameDelay
ShootDelay

Enemys
EnemySpawn
EnemyTotal
FrameDelay	min/max
StartDelay
DecrementDelay	how much to decrease each time
VelocityX?
VelocityY?

IMPORTANT
Must replace initial target 64x64 in spritesheet with new one used for the color collision detection 56x56!!

Placeholder for 3Dcity coming soon!
This is a test line

Spritesheet here
E:\Steven\XNA\3Dcity\SpriteSheet01
E:\Steven\XNA\3Dcity\SpriteSheet02
E:\Steven\XNA\3Dcity\Title

Measurements on Background
Y = 3 + 70 + 5 = 78
X = 6

Therefore make icons 70x70
Ensure that the colors are not white LHS / RHS


Multiple touches
CheckHorz
CheckVert

InputManager.Update()

Get touchLocation.Count
if (0 == touchLocation.Count)
{
	// obv. no touch(es) to detect
	continue;
}

// otherwise there must be at least one touch
horz = 0
foreach (touch in touches)
{
	state= GetTouchState(touch)
	if (!(Pressed == state || Moved == state))
	{
		// not press or move then continue
		continue
	}
	
	posn = GetPosition(touch);
	horz = controlManager.CheckHorz(posn);		// joypadCollision
}

if (0 == horz)
{
	// no touch in joypadCollision
	return;
}

// otherwise this is the most up-to-date touch press / move


LEVELS
https://www.giantbomb.com/space-harrier/3030-6036/
Stage 1: Moot
Stage 2: Geeza
Stage 3: Amar
Stage 4: Cieciel
Stage 5: Bonus Stage	BONUS
Stage 6: Olisis
Stage 7: Lucasia
Stage 8: Ida
Stage 9: Revi
Stage 10: Minia
Stage 11: Parms
Stage 12: Bonus Stage	BONUS
Stage 13: Drail
Stage 14: Asute
Stage 15: Vicel
Stage 16: Natura
Stage 17: Nark
Stage 18: Absymbel


SELECT DIFFICULTY
SELECT LEVEL NAME
[18]>> ABSYMBEL

[01]
MOOT

[07]
LUCASIA

[18]
ABSYMBEL



TODO
StorageManager
Ensure LoadContent() gets called OnActivated BEFORE IconManager LoadContent()
because icon manager updates the correct image for sound on / off


COLLISION detection algorithm
Enemys vs Target

if not enemys(boundingBox) intersect target(boundingBox) then quit
if not top or bottom intersect then quit
calculate midpoint enemy + target
if distanceSquared <= range then collide

Biggest value 100 Easy
Biggest value  90 Hard
check distance value between 2x midpoints	enemy + target
if value < range then dead else miss

Range = 80 is good min / max value

Maximum range Hard = 90	left / right couple of pixels away [tough!] but includes more wings collision

Bullet lookup slot
pos = {X:316 Y:276}
pos = {X:288 Y:248}


Sine Waves code
for (int d = 0; d < 360; d++)
{
	float r = MathHelper.ToRadians(d);
	double s = Math.Sin(r);
	double a = Math.Round(s, 2);
	string t = a.ToString();
	System.Diagnostics.Debug.WriteLine(t);
}