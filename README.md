Condor soaring simulator (v2)
ref: www.condorsoaring.com/
ref http://www.condorsoaring.com/manual/ UDP data
Checkout current issue of Sailplane and Gliding, August-September- 5 pages of experiments, false trails...

Simple program that collects the Condor UDP output
and converts it to output to a serial port
so the Condor Data can be processed externally.

Basis for a homesim
- Condor --> Arduino
- Arduino --> Condor is not possible yet
- Map keys, joysticks etc for inputs in a HID compatible device
Generic UDP output

Condor can stream data to external applications using UDP protocol.
UDP.ini

UDP output is enabled by setting »Enabled=1« parameter in the »UDP.ini« file found in Condor installation directory:

[General]
 Enabled=1

[Connection]
 Host=127.0.0.1
 Port=55278

[Misc]
 SendIntervalMs=1
 ExtendedData=0
 ExtendedData1=0
 LogToFile=0

In the same file host address and port are also set. Send rate is controlled by SendIntervalMs parameter which specifies the time interval between two consecutive data packets. Some additional parameters are available if ExtendedData or ExtendedData1 are enabled. The output can also be logged to file for debug purposes by setting the »LogToFile=1« parameter.
UDP Packet data

The data packet is an ASCII stream of ‘parameter=value’ pairs with the following parameters
		
time	    in-game display time	      decimal hours
slipball 	slip ball deflection angle	rad
altitude 	altimeter reading	          m or ft according to units selected
vario	    pneumatic variometer reading	  m/s
evario 	  electronic variometer reading	m/s
nettovario 	netto variometer value	  m/s
integrator 	integrator value	        m/s
compass 	compass reading	            degrees
slipball	slip ball deflection angle	rad
turnrate 	turn indicator reading	    rad/s
yawstringangle 	yawstring angle 	    rad
radiofrequency 	radio frequency 	    MHz
yaw	          yaw	rad
pitch	pitch	  rad
bank	bank	rad
quaternionx 	quaternion x 	/
quaterniony	quaternion y 	/
quaternionz	quaternion z	/
ax 	acceleration vector x	m/s2
ay	acceleration vector y	m/s2
az	acceleration vector z	m/s2
vx	speed vector x	m/s
vy	speed vector y	m/s
vz	speed vector z	m/s
rollrate	roll rate (local system)	rad/s
pitchrate	pitch rate (local system) y	rad/s
yawrate	yaw rate (local system) z	rad/s
gforce	g forces	/
height *	height of cg above ground	m
wheelheight *	height of wheel above ground	m
turbulencestrength *	turbulence strength	/
surfaceroughness *	surface roughness	/
hudmessages *	HUD message text	separated by ;
		flaps ** flaps position index : 0=most negative to MAXFLAPS-1
		MC ** MacCready setting m/s
		water ** Water ballast content kg
Please note: The hudmessages do not show properly. I suspect a bug with Condor. I have sent an email to Condor
