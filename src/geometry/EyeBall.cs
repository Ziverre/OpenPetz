using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class EyeBall : MeshInstance2D
{
	private ImmediateMesh immediateMesh;
	//To do: Merge this with this.Material
	private ShaderMaterial material;

	public Texture2D texture;
	public Texture2D palette;

	public int diameter;
	public int color_index;
	public int fuzz;
	public int outline_width;
	public int outline_color;
	public Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);

	public EyeBall()
	{

	}

	public EyeBall(Texture2D texture, Texture2D palette, int diameter, int color_index, int fuzz, int outline_width, int outline_color)
	{
		this.texture = texture;
		this.palette = palette;
		this.diameter = diameter;
		this.color_index = color_index;
		this.fuzz = fuzz;
		this.outline_width = outline_width;
		this.outline_color = outline_color;
	}

	public override void _Ready()
	{

		this.immediateMesh = new ImmediateMesh();
		//this.material = (ShaderMaterial)GD.Load<ShaderMaterial>("res://shaders/ball_shader.tres").Duplicate(true);
		this.material = ShaderManager.FetchShaderMaterial("eyeball");

		//this.immediateMesh.SurfaceSetMaterial(0, material); //is it necessary?

		this.Mesh = this.immediateMesh;
		this.Material = this.material;

		//Set Material uniform parameters

		this.material.SetShaderParameter("fuzz", fuzz);
		this.material.SetShaderParameter("diameter", diameter);
		this.material.SetShaderParameter("outline_width", outline_width);

		this.material.SetShaderParameter("color_index", color_index);
		this.material.SetShaderParameter("outline_color", outline_color);
		material.SetShaderParameter("transparent_color_index", 1);

		this.material.SetShaderParameter("tex", texture);
		this.material.SetShaderParameter("palette", palette);

		this.material.SetShaderParameter("center", this.GlobalPosition);
		
		//
		
		immediateMesh.ClearSurfaces();
		immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Triangles);

		//To do: find out whether this belongs in _Ready or _Process
		drawQuad(diameter + fuzz);

		immediateMesh.SurfaceEnd();
	}


	public override void _Process(double dt)
	{
		material.SetShaderParameter("center", this.GlobalPosition);

		 
		// ROTATION TEST CODE
		
		//rotation.X += 0.0625f;
		rotation.Y += 0.0625f;
		//rotation.Z += 0.0625f;

	}

	private void drawQuad(int size)
	{
		immediateMesh.SurfaceSetUV(new Vector2(0, 1));
		immediateMesh.SurfaceAddVertex(new Vector3(-1 * size, -1 * size, 0));

		immediateMesh.SurfaceSetUV(new Vector2(0, 0));
		immediateMesh.SurfaceAddVertex(new Vector3(-1 * size, size, 0));

		immediateMesh.SurfaceSetUV(new Vector2(1, 1));
		immediateMesh.SurfaceAddVertex(new Vector3(size, size, 0));


		immediateMesh.SurfaceSetUV(new Vector2(0, 128));
		immediateMesh.SurfaceAddVertex(new Vector3(size, -1 * size, 0));

		immediateMesh.SurfaceSetUV(new Vector2(0, 0));
		immediateMesh.SurfaceAddVertex(new Vector3(-1 * size, -1 * size, 0));

		immediateMesh.SurfaceSetUV(new Vector2(128, 128));
		immediateMesh.SurfaceAddVertex(new Vector3(size, size, 0));

	}


}
