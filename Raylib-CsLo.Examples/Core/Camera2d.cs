﻿// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] 
// [!!] Copyright ©️ Raylib-CsLo and Contributors. 
// [!!] This file is licensed to you under the MPL-2.0.
// [!!] See the LICENSE file in the project root for more info. 
// [!!] ------------------------------------------------- 
// [!!] The code and 100+ examples are here! https://github.com/NotNotTech/Raylib-CsLo 
// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!]  [!!] [!!] [!!] [!!]

/*******************************************************************************************
*
*   raylib [core] example - 2d camera
*
*   This example has been created using raylib 1.5 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Copyright (c) 2016 Ramon Santamaria (@raysan5)
*
********************************************************************************************/

namespace Raylib_CsLo.Examples.Core;

public static class Camera2d
{
	private const int MAX_BUILDINGS = 100;
	public static int main()
	{
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		InitWindow(screenWidth, screenHeight, "raylib [core] example - 2d camera");

		Rectangle player = new( 400, 280, 40, 40 );
		Rectangle[] buildings = new Rectangle[MAX_BUILDINGS];
		var buildColors = new Color[MAX_BUILDINGS];

		int spacing = 0;

		for (int i = 0; i < MAX_BUILDINGS; i++)
		{
			buildings[i].width = (float)GetRandomValue(50, 200);
			buildings[i].height = (float)GetRandomValue(100, 800);
			buildings[i].y = screenHeight - 130.0f - buildings[i].height;
			buildings[i].x = -6000.0f + spacing;

			spacing += (int)buildings[i].width;

			buildColors[i] =new( GetRandomValue(200, 240), GetRandomValue(200, 240), GetRandomValue(200, 250), 255 );
		}

		Camera2D camera;// = { 0 };
		camera.target = new( player.x + 20.0f, player.y + 20.0f );
		camera.offset = new( screenWidth / 2.0f, screenHeight / 2.0f );
		camera.rotation = 0.0f;
		camera.zoom = 1.0f;

		SetTargetFPS(60);                   // Set our game to run at 60 frames-per-second
											//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())        // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------

			// Player movement
			if (IsKeyDown(KEY_RIGHT)) player.x += 2;
			else if (IsKeyDown(KEY_LEFT)) player.x -= 2;

			// Camera target follows player
			camera.target = new(player.x + 20, player.y + 20 );

			// Camera rotation controls
			if (IsKeyDown(KEY_A)) camera.rotation--;
			else if (IsKeyDown(KEY_S)) camera.rotation++;

			// Limit camera rotation to 80 degrees (-40 to 40)
			if (camera.rotation > 40) camera.rotation = 40;
			else if (camera.rotation < -40) camera.rotation = -40;

			// Camera zoom controls
			camera.zoom += ((float)GetMouseWheelMove() * 0.05f);

			if (camera.zoom > 3.0f) camera.zoom = 3.0f;
			else if (camera.zoom < 0.1f) camera.zoom = 0.1f;

			// Camera reset (zoom and rotation)
			if (IsKeyPressed(KEY_R))
			{
				camera.zoom = 1.0f;
				camera.rotation = 0.0f;
			}
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(RAYWHITE);

			BeginMode2D(camera);

			DrawRectangle(-6000, 320, 13000, 8000, DARKGRAY);

			for (int i = 0; i < MAX_BUILDINGS; i++) DrawRectangleRec(buildings[i], buildColors[i]);

			DrawRectangleRec(player, RED);

			DrawLine((int)camera.target.X, -screenHeight * 10, (int)camera.target.X, screenHeight * 10, GREEN);
			DrawLine(-screenWidth * 10, (int)camera.target.Y, screenWidth * 10, (int)camera.target.Y, GREEN);

			EndMode2D();

			DrawText("SCREEN AREA", 640, 10, 20, RED);

			DrawRectangle(0, 0, screenWidth, 5, RED);
			DrawRectangle(0, 5, 5, screenHeight - 10, RED);
			DrawRectangle(screenWidth - 5, 5, 5, screenHeight - 10, RED);
			DrawRectangle(0, screenHeight - 5, screenWidth, 5, RED);

			DrawRectangle(10, 10, 250, 113, Fade(SKYBLUE, 0.5f));
			DrawRectangleLines(10, 10, 250, 113, BLUE);

			DrawText("Free 2d camera controls:", 20, 20, 10, BLACK);
			DrawText("- Right/Left to move Offset", 40, 40, 10, DARKGRAY);
			DrawText("- Mouse Wheel to Zoom in-out", 40, 60, 10, DARKGRAY);
			DrawText("- A / S to Rotate", 40, 80, 10, DARKGRAY);
			DrawText("- R to reset Zoom and Rotation", 40, 100, 10, DARKGRAY);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		CloseWindow();        // Close window and OpenGL context
							  //--------------------------------------------------------------------------------------

		return 0;
	}
}

