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
	getDead();
	//Serial.println("TEST");
	//Serial.flush();
	while(dead)
	{
		getDead();
		if (Serial.read() == 0x4F)
		{
			dead = false;
		}
	}
}

void getDead()
{
	if (Serial.read() == 0x5F)
	{
		Serial.print(dead?1:0);
	}
}

