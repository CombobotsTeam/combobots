﻿using System.Collections;
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
		{"end", ""},
		{"Scene03", ""},
		{"WhitePawn", "Well done Finn! I knew it ! You are the chosen one !"},
		{"Finn", "The chosen what ?"},
		{"WhitePawn", "Nevermind..."},
		{"WhitePawn", "Wouah ! The robots are coming from the forest !"},
		{"end", ""},
		{"Scene04", ""},
		{"BlackBishop", "Nyahahahahahahahah, you succeded to come to me nyahahahah!"},
		{"Finn", "Are you okay ?"},
		{"BlackBishop", "NO, I'M NOT, YOU KILLED MY FRIEND PAWNIE"},
		{"Finn", "What ? I didn't see any Pony there."},
		{"BlackBishop", "You fool ! Pawnie was a surname for my childhood friend BlackPawn :'("},
		{"Finn", "You childhood friend was a Pony ? I think you should let me go, you don't seem to feel very well"},
		{"BlackBishop", "Enough of that ! Prepare to die ! I swear I will have my revenge on Pawnie !"},
		{"end", ""},
		{"Scene05", ""},
		{"WhitePawn","Waw Finn, you... You're saving the earth, you know that ?"},
		{"WhitePawn","I have completely trust in you"},
		{"WhitePawn","I think it's time for you"},
		{"WhitePawn","to learn"},
		{"WhitePawn","my"},
		{"WhitePawn","secret !"},
		{"Finn","Wow, so you have a secret ! I knew it !"},
		{"Finn","What is it ? You like men ? You are a secret idol ?"},
		{"Finn","Or maybe you don't like chocolate ? Cause chocolate is black !"},
		{"Finn","Black like Black Kingdom ! Am I right ?"},
		{"WhitePawn","Maybe you're not ready after all..."},
		{"WhitePawn","Anyway ! I can ear some robots in the desert !"},
		{"end", ""},
		{"Scene06", ""},
		{"BlackRook","Intruder !"},
		{"BlackRook","Destroy !"},
		{"Finn","What ? Me ?"},
		{"Finn","Could you speak correctly please ?"},
		{"BlackRook","You !"},
		{"BlackRook","Irrespectful !"},
		{"BlackRook","Must !"},
		{"BlackRook","Destroy !"},
		{"Finn","I see... You're too stupid for that"},
		{"BlackRook","Not stupid !"},
		{"BlackRook","Just !"},
		{"BlackRook","Little brain !"},
		{"BlackRook","Big strenght !"},
		{"BlackRook","Ah ! Ah ! Ah !"},
		{"Finn","Okay, if you insist, let's fight then..."},
		{"end", ""},
		{"Scene07", ""},
		{"WhitePawn","..."},
		{"Finn","..."},
		{"WhitePawn","..."},
		{"Finn","..."},
		{"WhitePawn","..."},
		{"Finn","..."},
		{"WhitePawn","..."},
		{"Finn","WHAT ???? Why are you looking at me like that ???"},
		{"WhitePawn","Mmmmh... Nothing ! :)"},
		{"end", ""},
		{"Scene08", ""},
		{"BlackKnight","Huuuuhuuhuuhuu"},
		{"Finn","Oh my god ! So the bishop was right !"},
		{"BlackKnight","Huuuuhuuhuuhuu"},
		{"Finn","There is a pony !"},
		{"BlackKnight","Huuuuhuuhuuhuu"},
		{"Finn","Hey pony ! Do you know how to talk ?"},
		{"BlackKnight","Huuuuhuuhuuhuu"},
		{"Finn","Obviously not..."},
		{"BlackKnight","Huuuuhuuhuuhuu"},
		{"Finn","I'm sorry, usually I don't like to fight with animals..."},
		{"BlackKnight","Huuuuhuuhuuhuu"},
		{"Finn","...But since you're juste made of metal..."},
		{"BlackKnight","Huuuuhuuhuuhuu"},
		{"end", ""}
	};

	public string[,] cutscenesSprite = new string[,] {
		{ "Intro", "Blank", "Blank", "Intro" },
		{ "Intro_2", "Blank", "Blank", "Intro_2" },
		{ "Scene01", "Finn", "WhitePawn", "Intro_2" },
		{ "Scene02", "Finn", "BlackPawn", "env1" },
		{ "Scene03", "Finn", "WhitePawn", "env2" },
		{ "Scene04", "Finn", "BlackBishop", "env2" },
		{ "Scene05", "Finn", "WhitePawn", "env3" },
		{ "Scene06", "Finn", "BlackRook", "env3" },
		{ "Scene07", "Finn", "WhitePawn", "env4" },
		{ "Scene08", "Finn", "BlackKnight", "env4" }
	};

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
