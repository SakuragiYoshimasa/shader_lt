using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test2 : MonoBehaviour {

	public Material material; //描画用
	public Material kernelMat; //計算用
	public Mesh sphere;
	private int w = 100;
	private int h = 100;
	private RenderTexture positionBuffer;
	uint[] _drawArgs = new uint[5]{0, 0, 0, 0, 0};
	ComputeBuffer _drawArgsBuffer;
	Bounds _bounds = new Bounds(Vector3.zero, Vector3.one * 4 * 32);
	MaterialPropertyBlock _props;
	
	void Start () {
		positionBuffer = test2.createRTBuffer(w, h);
		_drawArgsBuffer = new ComputeBuffer(1, 5 * sizeof(uint), ComputeBufferType.IndirectArguments);
		_drawArgs[0] = (uint)sphere.GetIndexCount(0);
		_drawArgs[1] = (uint)10000;
		_drawArgsBuffer.SetData(_drawArgs);
		_props = new MaterialPropertyBlock();
        _props.SetFloat("_UniqueID", Random.value);
	}
	
	void Update () {
		UpdatePosition();	
		RenderSpheres();
	}

	void UpdatePosition(){
		kernelMat.SetFloat("_RealTime", Time.realtimeSinceStartup);
        Graphics.Blit (null, positionBuffer, kernelMat, 0);
    }

	void RenderSpheres(){
		_props.SetFloat("_UniqueID", Random.value);
		material.SetTexture("_PositionBuffer", positionBuffer);
		material.SetColor("_Color", new Color(1.0f,0,0,1.0f));
		material.SetFloat("_RealTime", Time.realtimeSinceStartup);
		Graphics.DrawMeshInstancedIndirect(sphere, 0, material, _bounds, _drawArgsBuffer, 0, _props);
	}

	static RenderTexture createRTBuffer(int w, int h){
        var format = RenderTextureFormat.ARGBFloat;
        var buffer = new RenderTexture(w, h, 0, format);
        buffer.hideFlags = HideFlags.DontSave;
        buffer.filterMode = FilterMode.Point;
        buffer.wrapMode = TextureWrapMode.Clamp;
        return buffer;
    }
}
