﻿// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] 
// [!!] Copyright ©️ Raylib-CsLo and Contributors. 
// [!!] This file is licensed to you under the MPL-2.0.
// [!!] See the LICENSE file in the project root for more info. 
// [!!] ------------------------------------------------- 
// [!!] The code and 100+ examples are here! https://github.com/NotNotTech/Raylib-CsLo 
// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!]  [!!] [!!] [!!] [!!]

/*******************************************************************************************
*
*   raylib [core] example - Gamepad input
*
*   NOTE: This example requires a Gamepad connected to the system
*         raylib is configured to work with the following gamepads:
*                - Xbox 360 Controller (Xbox 360, Xbox One)
*                - PLAYSTATION(R)3 Controller
*         Check raylib.h for buttons configuration
*
*   This example has been created using raylib 2.5 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2013-2019 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

//# include "raylib.h"

//// NOTE: Gamepad name ID depends on drivers and OS
//#define XBOX360_LEGACY_NAME_ID  "Xbox Controller"
//#if defined(PLATFORM_RPI)
//#define XBOX360_NAME_ID     "Microsoft X-Box 360 pad"
//#define PS3_NAME_ID         "PLAYSTATION(R)3 Controller"
//#else
//#define XBOX360_NAME_ID     "Xbox 360 Controller"
//#define PS3_NAME_ID         "PLAYSTATION(R)3 Controller"
//#endif
namespace Raylib_CsLo.Examples.Core;

using System;
using static Consts;
public static class Consts
{
	public static string XBOX360_LEGACY_NAME_ID = "Xbox Controller";
	//public static string XBOX360_NAME_ID = "Microsoft X-Box 360 pad";
	//public static string PS3_NAME_ID = "PLAYSTATION(R)3 Controller";
	public static string XBOX360_NAME_ID = "Xbox 360 Controller";
	public static string PS3_NAME_ID = "PLAYSTATION(R)3 Controller";
}


public static class GamepadInput
{

	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		SetConfigFlags(FLAG_MSAA_4X_HINT);  // Set MSAA 4X hint before windows creation

		InitWindow(screenWidth, screenHeight, "raylib [core] example - gamepad input");

		Texture2D texPs3Pad = LoadTexture("resources/ps3.png");
		Texture2D texXboxPad = LoadTexture("resources/xbox.png");

		SetTargetFPS(60);               // Set our game to run at 60 frames-per-second
										//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())    // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			// ...
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			if (IsGamepadAvailable(0))
			{
				var cname = GetGamepadName_(0);
				DrawText(TextFormat("GP1: %s", GetGamepadName_(0)), 10, 10, 10, BLACK);

				if (TextIsEqual(GetGamepadName_(0), XBOX360_NAME_ID) || TextIsEqual(GetGamepadName_(0), XBOX360_LEGACY_NAME_ID))
				{
					DrawTexture(texXboxPad, 0, 0, DARKGRAY);

					// Draw buttons: xbox home
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE)) DrawCircle(394, 89, 19, RED);

					// Draw buttons: basic
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_RIGHT)) DrawCircle(436, 150, 9, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_LEFT)) DrawCircle(352, 150, 9, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_LEFT)) DrawCircle(501, 151, 15, BLUE);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_DOWN)) DrawCircle(536, 187, 15, LIME);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT)) DrawCircle(572, 151, 15, MAROON);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_UP)) DrawCircle(536, 115, 15, GOLD);

					// Draw buttons: d-pad
					DrawRectangle(317, 202, 19, 71, BLACK);
					DrawRectangle(293, 228, 69, 19, BLACK);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_UP)) DrawRectangle(317, 202, 19, 26, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_DOWN)) DrawRectangle(317, 202 + 45, 19, 26, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_LEFT)) DrawRectangle(292, 228, 25, 19, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_RIGHT)) DrawRectangle(292 + 44, 228, 26, 19, RED);

					// Draw buttons: left-right back
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_TRIGGER_1)) DrawCircle(259, 61, 20, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_TRIGGER_1)) DrawCircle(536, 61, 20, RED);

					// Draw axis: left joystick
					DrawCircle(259, 152, 39, BLACK);
					DrawCircle(259, 152, 34, LIGHTGRAY);
					DrawCircle(259 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_X) * 20),
							   152 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_Y) * 20), 25, BLACK);

					// Draw axis: right joystick
					DrawCircle(461, 237, 38, BLACK);
					DrawCircle(461, 237, 33, LIGHTGRAY);
					DrawCircle(461 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_X) * 20),
							   237 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_Y) * 20), 25, BLACK);

					// Draw axis: left-right triggers
					DrawRectangle(170, 30, 15, 70, GRAY);
					DrawRectangle(604, 30, 15, 70, GRAY);
					DrawRectangle(170, 30, 15, (((1 + (int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_TRIGGER)) / 2) * 70), RED);
					DrawRectangle(604, 30, 15, (((1 + (int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_TRIGGER)) / 2) * 70), RED);

					//DrawText(TextFormat("Xbox axis LT: %02.02f", GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_TRIGGER)), 10, 40, 10, BLACK);
					//DrawText(TextFormat("Xbox axis RT: %02.02f", GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_TRIGGER)), 10, 60, 10, BLACK);
				}
				else if (TextIsEqual(GetGamepadName_(0), PS3_NAME_ID))
				{
					DrawTexture(texPs3Pad, 0, 0, DARKGRAY);

					// Draw buttons: ps
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE)) DrawCircle(396, 222, 13, RED);
					
					// Draw buttons: basic
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_LEFT)) DrawRectangle(328, 170, 32, 13, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_MIDDLE_RIGHT)) DrawTriangle(new( 436, 168), new( 436, 185), new(464, 177), RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_UP)) DrawCircle(557, 144, 13, LIME);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_RIGHT)) DrawCircle(586, 173, 13, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_DOWN)) DrawCircle(557, 203, 13, VIOLET);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_FACE_LEFT)) DrawCircle(527, 173, 13, PINK);

					// Draw buttons: d-pad
					DrawRectangle(225, 132, 24, 84, BLACK);
					DrawRectangle(195, 161, 84, 25, BLACK);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_UP)) DrawRectangle(225, 132, 24, 29, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_DOWN)) DrawRectangle(225, 132 + 54, 24, 30, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_LEFT)) DrawRectangle(195, 161, 30, 25, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_FACE_RIGHT)) DrawRectangle(195 + 54, 161, 30, 25, RED);

					// Draw buttons: left-right back buttons
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_LEFT_TRIGGER_1)) DrawCircle(239, 82, 20, RED);
					if (IsGamepadButtonDown(0, GAMEPAD_BUTTON_RIGHT_TRIGGER_1)) DrawCircle(557, 82, 20, RED);

					// Draw axis: left joystick
					DrawCircle(319, 255, 35, BLACK);
					DrawCircle(319, 255, 31, LIGHTGRAY);
					DrawCircle(319 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_X) * 20),
							   255 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_Y) * 20), 25, BLACK);

					// Draw axis: right joystick
					DrawCircle(475, 255, 35, BLACK);
					DrawCircle(475, 255, 31, LIGHTGRAY);
					DrawCircle(475 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_X) * 20),
							   255 + ((int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_Y) * 20), 25, BLACK);

					// Draw axis: left-right triggers
					DrawRectangle(169, 48, 15, 70, GRAY);
					DrawRectangle(611, 48, 15, 70, GRAY);
					DrawRectangle(169, 48, 15, (((1 - (int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_LEFT_TRIGGER)) / 2) * 70), RED);
					DrawRectangle(611, 48, 15, (((1 - (int)GetGamepadAxisMovement(0, GAMEPAD_AXIS_RIGHT_TRIGGER)) / 2) * 70), RED);
				}
				else
				{
					DrawText("- GENERIC GAMEPAD -", 280, 180, 20, GRAY);

					// TODO: Draw generic gamepad
				}

				DrawText(TextFormat("DETECTED AXIS [%i]:", GetGamepadAxisCount(0)), 10, 50, 10, MAROON);

				for (int i = 0; i < GetGamepadAxisCount(0); i++)
				{
					DrawText(TextFormat("AXIS %i: %.02f", i, GetGamepadAxisMovement(0, i)), 20, 70 + 20 * i, 10, DARKGRAY);
				}

				if (GetGamepadButtonPressed() != -1) DrawText(TextFormat("DETECTED BUTTON: %i", GetGamepadButtonPressed()), 10, 430, 10, RED);
				else DrawText("DETECTED BUTTON: NONE", 10, 430, 10, GRAY);
			}
			else
			{
				DrawText("GP1: NOT DETECTED", 10, 10, 10, GRAY);

				DrawTexture(texXboxPad, 0, 0, LIGHTGRAY);
			}

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadTexture(texPs3Pad);
		UnloadTexture(texXboxPad);

		CloseWindow();        // Close window and OpenGL context
							  //--------------------------------------------------------------------------------------

		return 0;
	}


}

