using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Ball : MeshInstance2D
{
	private Mesh ballMesh;
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

	public Ball()
	{

	}

	public Ball(Texture2D texture, Texture2D palette, int diameter, int color_index, int fuzz, int outline_width, int outline_color)
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

		this.ballMesh = MeshManager.FetchDefaultMesh();

		this.material = ShaderManager.FetchShaderMaterial("ball");

		this.Mesh = this.ballMesh;
		this.Material = this.material;

		//Set Material uniform parameters

		this.material.SetShaderParameter(StringManager.S("fuzz"), fuzz);
		this.material.SetShaderParameter(StringManager.S("diameter"), diameter);
		this.material.SetShaderParameter(StringManager.S("outline_width"), outline_width);

		this.material.SetShaderParameter(StringManager.S("color_index"), color_index);
		this.material.SetShaderParameter(StringManager.S("outline_color"), outline_color);
		material.SetShaderParameter(StringManager.S("transparent_color_index"), 1);

		this.material.SetShaderParameter(StringManager.S("tex"), texture);
		this.material.SetShaderParameter(StringManager.S("palette"), palette);

		this.material.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);
	}


	public override void _Process(double dt)
	{
		material.SetShaderParameter(StringManager.S("center"), this.GlobalPosition);
	}

}
