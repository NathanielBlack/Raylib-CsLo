﻿// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] 
// [!!] Copyright ©️ Raylib-CsLo and Contributors. 
// [!!] This file is licensed to you under the MPL-2.0.
// [!!] See the LICENSE file in the project root for more info. 
// [!!] ------------------------------------------------- 
// [!!] The code and 100+ examples are here! https://github.com/NotNotTech/Raylib-CsLo 
// [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!] [!!]  [!!] [!!] [!!] [!!]

namespace Raylib_CsLo.Examples.Shaders;

/*******************************************************************************************
*
*   raylib [shaders] example - fog
*
*   NOTE: This example requires raylib OpenGL 3.3 or ES2 versions for shaders support,
*         OpenGL 1.1 does not support shaders, recompile raylib to OpenGL 3.3 version.
*
*   NOTE: Shaders used in this example are #version 330 (OpenGL 3.3).
*
*   This example has been created using raylib 2.5 (www.raylib.com)
*   raylib is licensed under an unmodified zlib/libpng license (View raylib.h for details)
*
*   Example contributed by Chris Camacho (@chriscamacho) and reviewed by Ramon Santamaria (@raysan5)
*
*   Chris Camacho (@chriscamacho -  http://bedroomcoders.co.uk/) notes:
*
*   This is based on the PBR lighting example, but greatly simplified to aid learning...
*   actually there is very little of the PBR example left!
*   When I first looked at the bewildering complexity of the PBR example I feared
*   I would never understand how I could do simple lighting with raylib however its
*   a testement to the authors of raylib (including rlights.h) that the example
*   came together fairly quickly.
*
*   Copyright (c) 2019 Chris Camacho (@chriscamacho) and Ramon Santamaria (@raysan5)
*
********************************************************************************************/
public unsafe static class Fog
{

	const int GLSL_VERSION = 330;
	public static int main()
	{
		var rLights = new Examples.RLights();
		// Initialization
		//--------------------------------------------------------------------------------------
		const int screenWidth = 800;
		const int screenHeight = 450;

		SetConfigFlags(FLAG_MSAA_4X_HINT);  // Enable Multi Sampling Anti Aliasing 4x (if available)
		InitWindow(screenWidth, screenHeight, "raylib [shaders] example - fog");

		// Define the camera to look into our 3d world
		Camera camera = new(
			new Vector3(2.0f, 2.0f, 6.0f),      // position
			new Vector3(0.0f, 0.5f, 0.0f),      // target
			new Vector3(0.0f, 1.0f, 0.0f),      // up
			45.0f, CAMERA_PERSPECTIVE);        // fov, type

		// Load models and texture
		Model modelA = LoadModelFromMesh(GenMeshTorus(0.4f, 1.0f, 16, 32));
		Model modelB = LoadModelFromMesh(GenMeshCube(1.0f, 1.0f, 1.0f));
		Model modelC = LoadModelFromMesh(GenMeshSphere(0.5f, 32, 32));
		Texture texture = LoadTexture("resources/texel_checker.png");

		// Assign texture to default model material
		modelA.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;
		modelB.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;
		modelC.materials[0].maps[(int)MATERIAL_MAP_DIFFUSE].texture = texture;

		// Load shader and set up some uniforms
		Shader shader = LoadShader(TextFormat("resources/shaders/glsl%i/base_lighting.vs", GLSL_VERSION),
								   TextFormat("resources/shaders/glsl%i/fog.fs", GLSL_VERSION));
		shader.locs[(int)SHADER_LOC_MATRIX_MODEL] = GetShaderLocation(shader, "matModel");
		shader.locs[(int)SHADER_LOC_VECTOR_VIEW] = GetShaderLocation(shader, "viewPos");

		// Ambient light level
		int ambientLoc = GetShaderLocation(shader, "ambient");
		SetShaderValue(shader, ambientLoc, new Vector4(0.2f, 0.2f, 0.2f, 1.0f), SHADER_UNIFORM_VEC4);

		float fogDensity = 0.15f;
		int fogDensityLoc = GetShaderLocation(shader, "fogDensity");
		SetShaderValue(shader, fogDensityLoc, &fogDensity, SHADER_UNIFORM_FLOAT);

		// NOTE: All models share the same shader
		modelA.materials[0].shader = shader;
		modelB.materials[0].shader = shader;
		modelC.materials[0].shader = shader;

		// Using just 1 point lights
		rLights.CreateLight(LIGHT_POINT, new Vector3(0, 2, 6), Vector3Zero(), WHITE, shader);

		SetCameraMode(camera, CAMERA_ORBITAL);  // Set an orbital camera mode

		SetTargetFPS(60);                       // Set our game to run at 60 frames-per-second
												//--------------------------------------------------------------------------------------

		// Main game loop
		while (!WindowShouldClose())            // Detect window close button or ESC key
		{
			// Update
			//----------------------------------------------------------------------------------
			UpdateCamera(&camera);              // Update camera

			if (IsKeyDown(KEY_UP))
			{
				fogDensity += 0.001f;
				if (fogDensity > 1.0f) fogDensity = 1.0f;
			}

			if (IsKeyDown(KEY_DOWN))
			{
				fogDensity -= 0.001f;
				if (fogDensity < 0.0f) fogDensity = 0.0f;
			}

			SetShaderValue(shader, fogDensityLoc, &fogDensity, SHADER_UNIFORM_FLOAT);

			// Rotate the torus
			modelA.transform = MatrixMultiply(modelA.transform, MatrixRotateX(-0.025f));
			modelA.transform = MatrixMultiply(modelA.transform, MatrixRotateZ(0.012f));

			// Update the light shader with the camera view position
			SetShaderValue(shader, shader.locs[(int)SHADER_LOC_VECTOR_VIEW], &camera.position.X, SHADER_UNIFORM_VEC3);
			//----------------------------------------------------------------------------------

			// Draw
			//----------------------------------------------------------------------------------
			BeginDrawing();

			ClearBackground(GRAY);

			BeginMode3D(camera);

			// Draw the three models
			DrawModel(modelA, Vector3Zero(), 1.0f, WHITE);
			DrawModel(modelB, new Vector3(-2.6f, 0, 0), 1.0f, WHITE);
			DrawModel(modelC, new Vector3(2.6f, 0, 0), 1.0f, WHITE);

			for (int i = -20; i < 20; i += 2) DrawModel(modelA, new Vector3((float)i, 0, 2), 1.0f, WHITE);

			EndMode3D();

			DrawText(TextFormat("Use KEY_UP/KEY_DOWN to change fog density [%.2f]", fogDensity), 10, 10, 20, RAYWHITE);

			EndDrawing();
			//----------------------------------------------------------------------------------
		}

		// De-Initialization
		//--------------------------------------------------------------------------------------
		UnloadModel(modelA);        // Unload the model A
		UnloadModel(modelB);        // Unload the model B
		UnloadModel(modelC);        // Unload the model C
		UnloadTexture(texture);     // Unload the texture
		UnloadShader(shader);       // Unload shader

		CloseWindow();              // Close window and OpenGL context
									//--------------------------------------------------------------------------------------

		return 0;
	}
}


