Shader "Bubblegum/Paint/Paint Damage" {
	Properties {
		[Header(Gloss Coat)] _Color ("Color", Vector) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0, 1)) = 0.5
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		[Header(Metallic Coat)] _SpecColor ("Specular Color", Vector) = (1,1,1,1)
		_SpecTex ("Specular Texture (RGB)", 2D) = "white" {}
		[Header (Detail (Scratches))] _DetailPower ("Detail Power", Range(0, 1)) = 0
		_DetailGloss ("Detail Smoothness", Range(0, 1)) = 1
		_DetailSpec ("Detail Specular", Vector) = (0.1,0.1,0.1,0.1)
		_DetailTex ("Detail (RGBA)", 2D) = "white" {}
		[Header(Cover (Dirt))] _CoverPower ("Cover Power", Range(0, 1)) = 0
		_CoverGloss ("Cover Smoothness", Range(0, 1)) = 1
		_CoverSpec ("Cover Specular", Vector) = (0,0,0,0)
		_CoverTex ("Cover (RGBA - Detail Coordinates)", 2D) = "white" {}
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "Diffuse"
}