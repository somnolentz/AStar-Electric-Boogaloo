Shader "Custom/TeleportingShader" {
    Properties {
        _MainTex ("Main Texture", 2D) = "white" {}
        _DistortionScale ("Distortion Scale", Range(0, 1)) = 0.1
        _ScanlineIntensity ("Scanline Intensity", Range(0, 1)) = 0.1
        _ScanlineCount ("Scanline Count", Range(0, 100)) = 10
        _ScanlineSpeed ("Scanline Speed", Range(0, 10)) = 1
       
    }

    SubShader {
        Tags
        {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }


            Blend SrcAlpha OneMinusSrcAlpha
        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            //assign variables here
            sampler2D _MainTex; 
            float _DistortionScale;
            float _ScanlineIntensity;
            float _ScanlineCount;
            float _ScanlineSpeed;
            
           


            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                //set distortion scale by using the texture coordinates.
                float2 offset = (i.uv - 0.5) * _DistortionScale;
                offset *= offset * (3.0 - 2.0 * offset);

                //setting rgb offsets
                float2 redOffset = offset * 0.9;
                float2 greenOffset = offset * 0.05;
                float2 blueOffset = offset * -0.3;

                //applying rgb offset to tex coordinates
                fixed4 redTex = tex2D(_MainTex, i.uv + redOffset);
                fixed4 greenTex = tex2D(_MainTex, i.uv + greenOffset);
                fixed4 blueTex = tex2D(_MainTex, i.uv + blueOffset);

                //getting final texture by assigning said textures by their r g and b values
                fixed4 finalTex = fixed4(redTex.r, greenTex.g, blueTex.b, redTex.a);
               
                //setting scanlines
                float scanlineCount = _ScanlineCount;
                float scanlineSpeed = _ScanlineSpeed;
                float scanline = step(frac(i.uv.y * scanlineCount - (_Time.y * scanlineSpeed)), _ScanlineIntensity);
                finalTex.rgb *= (1.0 - scanline);

                   
                //gets fractional part of a number. creates looping pattern cus miltiplied by time.
                 //^^ step retruns 1 if the argument is greater or equal to the second argument, were comparing the fraction here. if less than equal scanline intensity the step will return 1, which results in black pixels
                //invert black and wwhite values. makes black pixels transparent and white pix opaque. multiplying it with final tex will add the scanlines.
                
                
                return finalTex;
            }
            ENDCG
        }
    }
   }