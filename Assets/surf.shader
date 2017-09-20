Shader "Instanced/surf" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_RealTime ("-", Float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard vertex:vert
        #pragma instancing_options procedural:setup

		struct Input {
			float2 uv;
		};

		#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
			sampler2D _PositionBuffer;
			fixed4 _Color;
			float _RealTime;
		#endif

		void vert(inout appdata_full v, out Input data){
            UNITY_INITIALIZE_OUTPUT(Input, data);
			#ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
			v.vertex.xyz += tex2Dlod(_PositionBuffer,float4(float(int(unity_InstanceID)/100)/ 100.0, float(int(unity_InstanceID) % 100)/100.0,0,0)).xyz;
			data.uv = float2(0,0);
			#endif
        }
        void setup(){
            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            #endif
        }
        void surf (Input IN, inout SurfaceOutputStandard o) {
            #ifdef UNITY_PROCEDURAL_INSTANCING_ENABLED
            o.Albedo = _Color.rgb;
            o.Metallic = 0.5;
            o.Smoothness = 0.5;
            o.Alpha = _Color.a + IN.uv.x;
            #endif
        }
		ENDCG
	}
	FallBack "Diffuse"
}
