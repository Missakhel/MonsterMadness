// Shader created with Shader Forge v1.40 
// Shader Forge (c) Freya Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.40;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,cpap:True,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32623,y:32706,varname:node_2865,prsc:2|emission-5425-OUT,alpha-4775-OUT;n:type:ShaderForge.SFN_Color,id:6665,x:31711,y:32770,ptovrint:False,ptlb:OuterColor,ptin:_OuterColor,varname:_Color,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7830189,c2:0.4006407,c3:0.1440459,c4:1;n:type:ShaderForge.SFN_Fresnel,id:7918,x:31786,y:32932,varname:node_7918,prsc:2|EXP-9403-OUT;n:type:ShaderForge.SFN_Lerp,id:1092,x:32041,y:32847,varname:node_1092,prsc:2|A-2761-RGB,B-6665-RGB,T-7918-OUT;n:type:ShaderForge.SFN_Color,id:2761,x:31729,y:32585,ptovrint:False,ptlb:InnerColor,ptin:_InnerColor,varname:node_2761,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9056604,c2:0,c3:0.08163568,c4:1;n:type:ShaderForge.SFN_Vector1,id:9403,x:31623,y:32966,varname:node_9403,prsc:2,v1:3;n:type:ShaderForge.SFN_Lerp,id:4775,x:32282,y:33250,varname:node_4775,prsc:2|A-5217-OUT,B-9909-OUT,T-7873-OUT;n:type:ShaderForge.SFN_Slider,id:5217,x:32021,y:33079,ptovrint:False,ptlb:InnerAlpha,ptin:_InnerAlpha,varname:node_5217,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:9909,x:31858,y:33228,ptovrint:False,ptlb:OuterAlpha,ptin:_OuterAlpha,varname:node_9909,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Vector1,id:9074,x:31596,y:33140,varname:node_9074,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Fresnel,id:7873,x:31786,y:33082,varname:node_7873,prsc:2|EXP-9074-OUT;n:type:ShaderForge.SFN_Tex2d,id:4423,x:31971,y:32343,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_4423,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ccddf4191d35b644bb1b85f3b3617df5,ntxv:0,isnm:False|UVIN-4870-UVOUT;n:type:ShaderForge.SFN_Add,id:5425,x:32369,y:32769,varname:node_5425,prsc:2|A-9115-OUT,B-1092-OUT;n:type:ShaderForge.SFN_Lerp,id:9115,x:32114,y:32547,varname:node_9115,prsc:2|A-6410-OUT,B-2761-RGB,T-4423-R;n:type:ShaderForge.SFN_Panner,id:4870,x:31748,y:32321,varname:node_4870,prsc:2,spu:0.1,spv:0|UVIN-7850-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7850,x:31530,y:32338,varname:node_7850,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector4,id:6410,x:31856,y:32494,varname:node_6410,prsc:2,v1:0.1226415,v2:0.03044588,v3:0.008677464,v4:0;proporder:6665-2761-5217-9909-4423;pass:END;sub:END;*/

Shader "Shader Forge/SHD_BombSpell" {
    Properties {
        _OuterColor ("OuterColor", Color) = (0.7830189,0.4006407,0.1440459,1)
        _InnerColor ("InnerColor", Color) = (0.9056604,0,0.08163568,1)
        _InnerAlpha ("InnerAlpha", Range(0, 1)) = 0
        _OuterAlpha ("OuterAlpha", Range(0, 1)) = 1
        _Noise ("Noise", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _OuterColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _InnerColor)
                UNITY_DEFINE_INSTANCED_PROP( float, _InnerAlpha)
                UNITY_DEFINE_INSTANCED_PROP( float, _OuterAlpha)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 _InnerColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _InnerColor );
                float4 node_9268 = _Time;
                float2 node_4870 = (i.uv0+node_9268.g*float2(0.1,0));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_4870, _Noise));
                float4 _OuterColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterColor );
                float3 emissive = (lerp(float4(0.1226415,0.03044588,0.008677464,0),float4(_InnerColor_var.rgb,0.0),_Noise_var.r)+lerp(_InnerColor_var.rgb,_OuterColor_var.rgb,pow(1.0-max(0,dot(normalDirection, viewDirection)),3.0))).rgb;
                float3 finalColor = emissive;
                float _InnerAlpha_var = UNITY_ACCESS_INSTANCED_PROP( Props, _InnerAlpha );
                float _OuterAlpha_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterAlpha );
                fixed4 finalRGBA = fixed4(finalColor,lerp(_InnerAlpha_var,_OuterAlpha_var,pow(1.0-max(0,dot(normalDirection, viewDirection)),1.5)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #pragma multi_compile_instancing
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma target 3.0
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            UNITY_INSTANCING_BUFFER_START( Props )
                UNITY_DEFINE_INSTANCED_PROP( float4, _OuterColor)
                UNITY_DEFINE_INSTANCED_PROP( float4, _InnerColor)
            UNITY_INSTANCING_BUFFER_END( Props )
            struct VertexInput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                UNITY_VERTEX_INPUT_INSTANCE_ID
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID( v );
                UNITY_TRANSFER_INSTANCE_ID( v, o );
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UNITY_SETUP_INSTANCE_ID( i );
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 _InnerColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _InnerColor );
                float4 node_5578 = _Time;
                float2 node_4870 = (i.uv0+node_5578.g*float2(0.1,0));
                float4 _Noise_var = tex2D(_Noise,TRANSFORM_TEX(node_4870, _Noise));
                float4 _OuterColor_var = UNITY_ACCESS_INSTANCED_PROP( Props, _OuterColor );
                o.Emission = (lerp(float4(0.1226415,0.03044588,0.008677464,0),float4(_InnerColor_var.rgb,0.0),_Noise_var.r)+lerp(_InnerColor_var.rgb,_OuterColor_var.rgb,pow(1.0-max(0,dot(normalDirection, viewDirection)),3.0))).rgb;
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
