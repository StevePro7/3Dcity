# 3Dcity
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