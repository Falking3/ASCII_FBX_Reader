# ASCII FBX READER

This is an ongoing project that I'm developing in my spare time to practice the C#/WPF principles that I've been learning recently, and to allow me to do some hands on research with the FBX file format. 
The final program is envisaged as a intermediate platform between the DCC program and engine which performs validation checks on the exported model.

## What is there currently?

The program is pretty bare bones. I've focussed on reading data from the file primarily, with it printing to a console app with some basic validation for stacked faces and flipped normals.

## What am I going to add?

The plan is to continue parsing the FBX data until I've got all the stored data available in the program. Then comes more validation - basic checks like missing UVs, normal modes which break Unity, zero length edges, stacked vertices. Also easily viewable stats (list of animations, tri/vert count stats, submeshs etc). The plan is for this to run through a nice WPF GUI as well. 
