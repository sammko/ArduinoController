void setup()
{
	Serial.begin(9600);
	while (Serial.available() == 0);
}

bool dead;

void loop()
{
	delay(50);
	if (Serial.read() == 0x3F)
	{
		dead = true;
	}
	Serial.println("Hi, TESTING");
	Serial.flush();
	while(dead)
	{
		if (Serial.read() == 0x4F)
		{
			dead = false;
		}
	}
}

