using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigurationFile : MonoBehaviour {

	public string[,] cutscenesDialog = new string[,]
	{
		{"Intro", ""},
		{"", "The story that I'm going to tell you..."},
		{"", "... is an unusual story of a little geek."},
		{"", "His name was Finn."},
		{"", "This story is going to tell you..."},
		{"", "... how he became the hero of earth..."},
		{"end", ""},
		{"Intro_2", ""},
		{"", "Everything started during a normal day."},
		{"", "Finn was playing video games like always."},
		{"", "But this time... the unexpected happened..."},
		{"", "Something just crashed in his room."},
		{"", "It was... a white pawn... from a chess game ??!!!"},
		{"", "This white pawn was alive... and apparently not from our world..."},
		{"end", ""},
		{"Scene01", ""},
		{"Finn", "WOW ! What happened in there ?"},
		{"WhitePawn", "Oh... Hum... Hello !"},
		{"Finn", "He... Hello..."},
		{"WhitePawn", "Well... Nice to meet you, I'm White Pawn, a white pawn from the white Kingdom!"},
		{"Finn", "What the... I have no idea of what you're talking about !"},
		{"WhitePawn", "Ok, let me explain you... I come from a world where White and Black Kingdoms used to live in peace"},
		{"WhitePawn", "But one day, the Black Kingdom attacked us... and destroyed us."},
		{"WhitePawn", "All my people are dead. I ran away but now the Black Kingdom is coming to take over the earth."},
		{"WhitePawn", "You are the chosen one ! You will save the earth and I'll take my revenge !"},
		{"WhitePawn", "Here is the most powerful weapon against their robot army!"},
		{"Finn", "But... why do you need me ? Why don't you use your weapon yourself ?"},
		{"WhitePawn", "Well... I don't have arms T.T ... Wait ! I ear them, they're coming !"},
		{"WhitePawn", "Time for you to shine !"},
		{"end", ""},
		{"Scene02", ""},
		{"BlackPawn", "Here you come stupid human ! I'll never let you go after what you did to my robots !"},
		{"Finn", "..."},
		{"BlackPawn", "Hey ! Don't ignore me !"},
		{"BlackPawn", "I'm the Black Pawn from the Black Kingdom ! I'll kick your a** !"},
		{"Finn", "..."},
		{"BlackPawn", "Please... "},
		{"BlackPawn", "Please... Human... (@_@) "},
		{"Finn", "... Oh sorry, I didn't see you, who are you ?"},
		{"BlackPawn", "Okay Human, you'll regret it, let's fight !"},
		{"end", ""}
	};

	public string[,] cutscenesSprite = new string[,] {
		{ "Intro", "Blank", "Blank", "Intro" },
		{ "Intro_2", "Blank", "Blank", "Intro_2" },
		{ "Scene01", "Finn", "WhitePawn", "Intro_2" },
		{ "Scene02", "Finn", "BlackPawn", "env1" }

	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
