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