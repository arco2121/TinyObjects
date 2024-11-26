namespace TinyObjects
{
    const Schell = 1
    
    const TinyPointer = {
        enable : () => {
            document.querySelectorAll("img").forEach(img => {
                img.style.cursor = "crosshair"
            })
        },
        disable : () => {
            document.querySelectorAll("img").forEach(img => {
                img.removeAttribute("cursor")
            })
        }
    }

    const Picker = () : TinyPicker => {
        return new TinyPicker(100,4)
    } 

    const Color = (data : string | number[]) : TinyColor => {
        return new TinyColor(data)
    } 

    class TinyColor
    {
        private RGB : number[]
        private HEX : string
        private HSL : number[]
        private LAB : number[]

        constructor(data : string | number[])
        {
            if(typeof data == "string")
            {
                this.HEX = data
                this.RGB = TinyColor.HEXToRGB(data)
                this.HSL = TinyColor.RGBToHSL(this.RGB)
                this.LAB = TinyColor.RGBToLAB(this.RGB)
                return
            }
            else if (Array.isArray(data) && data.length === 3)
            {
                this.RGB = data
                this.HEX = TinyColor.RGBToHEX(data)
                this.HSL = TinyColor.RGBToHSL(this.RGB)
                this.LAB = TinyColor.RGBToLAB(this.RGB)
                return
            }

            throw new Error("Input must be a HEX code string such as #FFFFFF or a number[R,G,B]")
        }

        Hex() : string
        {
            return this.HEX
        }

        Rgb() : number[]
        {
            return this.RGB
        }

        Hsl() : number[]
        {
            return this.HSL.slice(1,3)
        }

        Lab() : number[]
        {
            return this.LAB
        }

        RgbString() : string
        {
            return "rgb(" + this.RGB[0] + " " + this.RGB[1] + " " + this.RGB[2] + ")"
        }

        HslString() : string
        {
            return "hsl(" + this.HSL[0] + " " + this.HSL[1] + " " + this.HSL[2] + ")"
        }

        LabString() : string
        {
            return "lab(" + this.LAB[0] + " " + this.LAB[1] + " " + this.LAB[2] + ")"
        }

        JSON() : string
        {
            return JSON.stringify({
                Color : this.Hex(),
                Validate : true
            })
        }

        Invert() : TinyColor
        {
            const rgb = this.RGB;
            
            const r = 255 - rgb[0];
            const g = 255 - rgb[1];
            const b = 255 - rgb[2];
            
            return new TinyColor([r, g, b]);
        }

        isColorLight() : boolean 
        {
            const rgb = this.Rgb();
            if (!rgb) return false
            const brightness = (rgb[0] * 299 + rgb[1] * 587 + rgb[2] * 114) / 1000;
            return brightness > 128;
        }

        AdjustBrightness(percent: number): TinyColor
        {
            const rgb = this.Rgb();
            const adjust = (value: number) => Math.min(255, Math.max(0, value * (1 + percent / 100)));
            const r = Math.round(adjust(rgb[0]));
            const g = Math.round(adjust(rgb[1]));
            const b = Math.round(adjust(rgb[2]));
        
            return new TinyColor([r, g, b]);
        }

        Mix(Color : TinyColor, count : number) : TinyColor[]
        {
            const start = this.Rgb();
            const end = Color.Rgb();
            if (!start || !end) return [];
            
            const stepColors: TinyColor[] = [];
            
            for (let i = 0; i <= count; i++) 
            {
                const r = Math.round(start[0] + (end[0] - start[0]) * (i / count));
                const g = Math.round(start[1] + (end[1] - start[1]) * (i / count));
                const b = Math.round(start[2] + (end[2] - start[2]) * (i / count));
            
                stepColors.push(new TinyColor([r, g, b]));
            }
            return stepColors
        }

        AnalogousColors(count : number, step : number = 30): TinyColor[]
        {
            const baseHsl = this.Hsl();
            const colors : TinyColor[] = [];
        
            for (let i = 0; i < count; i++) 
            {
                const newHue = (baseHsl[0] + step * i) % 360
                const rgb = TinyColor.HSLToRGB([newHue, baseHsl[1], baseHsl[2]])
                colors.push(new TinyColor([rgb[0], rgb[1], rgb[2]]))
            }
        
            return colors;
        }

        GrayscaleVariants(steps: number = 5): TinyColor[] 
        {
            const rgb = this.Rgb();
            const grayscaleValues: TinyColor[] = [];
        
            for (let i = 0; i < steps; i++) {
            const gray = Math.round((rgb[0] + rgb[1]+ rgb[3]) / 3 * (i / (steps - 1)));
            grayscaleValues.push(new TinyColor([gray, gray, gray]));
            }
            return grayscaleValues;
        }

        DeltaE2000Distance(Color : TinyColor) : number 
        {
            const [L1, A1, B1 ] = this.Lab();
            const [L2, A2, B2 ] = Color.Lab();
        
            const deltaL = L2 - L1;
            const L_ = (L1 + L2) / 2;
        
            const C1 = Math.sqrt(A1 ** 2 + B1 ** 2);
            const C2 = Math.sqrt(A2 ** 2 + B2 ** 2);
            const C_ = (C1 + C2) / 2;
            
            const G = 0.5 * (1 - Math.sqrt((C_ ** 7) / (C_ ** 7 + 25 ** 7)));
            const a1Prime = A1 * (1 + G);
            const a2Prime = A2 * (1 + G);
        
            const C1Prime = Math.sqrt(a1Prime ** 2 + B1 ** 2);
            const C2Prime = Math.sqrt(a2Prime ** 2 + B2 ** 2);
            const deltaCPrime = C2Prime - C1Prime;
        
            const h1Prime = Math.atan2(B1, a1Prime) * (180 / Math.PI);
            const h2Prime = Math.atan2(B2, a2Prime) * (180 / Math.PI);
        
            const deltahPrime = h2Prime - h1Prime;
            const deltaHPrime = 2 * Math.sqrt(C1Prime * C2Prime) * Math.sin(this.DEGToRAD(deltahPrime) / 2);
        
            const T = 1 - 0.17 * Math.cos(this.DEGToRAD(h1Prime - 30)) + 0.24 * Math.cos(this.DEGToRAD(2 * h1Prime))
                    + 0.32 * Math.cos(this.DEGToRAD(3 * h1Prime + 6)) - 0.20 * Math.cos(this.DEGToRAD(4 * h1Prime - 63));
        
            const SL = 1 + (0.015 * (L_ - 50) ** 2) / Math.sqrt(20 + (L_ - 50) ** 2);
            const SC = 1 + 0.045 * C_;
            const SH = 1 + 0.015 * C_ * T;
        
            const deltaTheta = 30 * Math.exp(-(((h1Prime - 275) / 25) ** 2));
            const RC = 2 * Math.sqrt((C_ ** 7) / (C_ ** 7 + 25 ** 7));
            const RT = -RC * Math.sin(this.DEGToRAD(2 * deltaTheta));
        
            return Math.sqrt((deltaL / SL) ** 2 + (deltaCPrime / SC) ** 2 + (deltaHPrime / SH) ** 2 + RT * (deltaCPrime / SC) * (deltaHPrime / SH));
        }

        RgbDistance(Color: TinyColor): number 
        {
            const rgb1 = this.Rgb()
            const rgb2 = Color.Rgb()
        
            if (!rgb1 || !rgb2) return 0;
        
            return Math.sqrt(
                Math.pow(rgb2[0] - rgb1[0], 2) + 
                Math.pow(rgb2[1]  - rgb1[1] , 2) + 
                Math.pow(rgb2[2]  - rgb1[2] , 2)
            )
        }

        FindClosest(Colors: TinyColor[]): TinyColor
        {
            let minDistance = Infinity;
            let closestColor : TinyColor = new TinyColor("#FFFFFF");
        
            for (const color of Colors) 
            {
                const distance = this.RgbDistance(color);
                if (distance < minDistance) 
                {
                    minDistance = distance;
                    closestColor = color;
                }
            }
        
            return closestColor;
        }

        GenerateGradient(Color: TinyColor, type : 'linear' | 'radial' = 'linear', angle : number = 90) : string 
        {   
            return type === 'linear'
            ? `linear-gradient(${angle}deg, ${this.Hex()}, ${Color.Hex()})`
            : `radial-gradient(circle, ${this.Hex()}, ${Color.Hex()})`;
        }      

        private DEGToRAD(degrees: number): number 
        {
            return degrees * (Math.PI / 180);
        }
        
        static RGBToHEX(RGB : number[]) : string
        {
            let res : any = RGB.map((color : number) => {
                if(color < 0 || color > 255)
                {
                    return "#FFFFFF"
                }
                let str : string = color.toString(16)
                str = "0".repeat(2 - str.length) + str
                return str
            })
            
            return "#" + res.join("")
        }
        
        static HEXToRGB(HEX : string) : number[]
        {
            HEX = HEX.replace(/^#/, '');
            if (HEX.length === 3) 
            {
                HEX = HEX.split('').map(char => char + char).join('');
            } 
            else if (HEX.length !== 6) 
            {
                return [255,255,255];
            }
            const r = parseInt(HEX.substring(0, 2), 16);
            const g = parseInt(HEX.substring(2, 4), 16);
            const b = parseInt(HEX.substring(4, 6), 16);
        
            return [r,g,b];
        }
        
        static RGBToHSL(RGB : number[]) : number[] 
        {
            let r = RGB[0] / 255;
            let g = RGB[1] / 255;
            let b = RGB[2] / 255;
        
            const max = Math.max(r, g, b);
            const min = Math.min(r, g, b);
            let h, s, l = (max + min) / 2;
        
            if (max === min) {
                h = s = 0; 
            } else {
                const d = max - min;
                s = l > 0.5 ? d / (2 - max - min) : d / (max + min); 
                switch (max) {
                    case r:
                        h = (g - b) / d + (g < b ? 6 : 0);
                        break;
                    case g:
                        h = (b - r) / d + 2;
                        break;
                    case b:
                        h = (r - g) / d + 4;
                        break;
                    default : 
                        h = 0
                }
                h = Math.round(h * 60)
            }
            s = Math.round(s * 100);
            l = Math.round(l * 100);
        
            return [h, s, l];
        }
        
        static HSLToRGB(HSL : number[]) : number[] 
        {
            let h = HSL[0];
            let s = HSL[1] / 100; 
            let l = HSL[2] / 100; 
        
            const k = (n: number) => (n + h / 30) % 12;
            const a = s * Math.min(l, 1 - l);
            const f = (n: number) => l - a * Math.max(-1, Math.min(k(n) - 3, 9 - k(n), 1));
            return [
                Math.round(f(0) * 255),
                Math.round(f(8) * 255),
                Math.round(f(4) * 255)  
            ];
        }    

        static RGBToLAB(RGB : number[]) : number[]
        {
            let [r,g,b] = RGB
            r /= 255; g /= 255; b /= 255;
            r = r > 0.04045 ? Math.pow((r + 0.055) / 1.055, 2.4) : r / 12.92;
            g = g > 0.04045 ? Math.pow((g + 0.055) / 1.055, 2.4) : g / 12.92;
            b = b > 0.04045 ? Math.pow((b + 0.055) / 1.055, 2.4) : b / 12.92;
        
            const x = (r * 0.4124564 + g * 0.3575761 + b * 0.1804375) / 0.95047;
            const y = (r * 0.2126729 + g * 0.7151522 + b * 0.0721750) / 1.00000;
            const z = (r * 0.0193339 + g * 0.1191920 + b * 0.9503041) / 1.08883;
        
            const xyzToLab = (t: number) => t > 0.008856 ? Math.pow(t, 1 / 3) : (7.787 * t) + (16 / 116);
            const L = (116 * xyzToLab(y)) - 16;
            const a = 500 * (xyzToLab(x) - xyzToLab(y));
            const bq = 200 * (xyzToLab(y) - xyzToLab(z));
        
            return [L, a, bq];
        }

        static GetRandom() : TinyColor
        {
            const r = Math.floor(Math.random() * 256);
            const g = Math.floor(Math.random() * 256);
            const b = Math.floor(Math.random() * 256);
            return new TinyColor([r, g, b])
        }

        static JSON(Color : string) : TinyColor
        {
            try
            {
                const COLOR = JSON.parse(Color)
                if(COLOR.Validate == true)
                {
                    return new TinyColor(COLOR.Color)
                }
                throw new Error("Not a color")
            }
            catch
            {
                throw new Error("Not a color")
            }
        }
    }

    class TinyPicker
    {
        private _ratio : number
        private _count : number

        constructor(ratio : number, count : number)
        {
            if(ratio == null || isNaN(ratio))
                throw new Error("Ratio must be a number")
            this._ratio = ratio
            if(count == null || count < 4 || isNaN(count))
                throw new Error("Count must be greater than 4")
            this._count = count
        }

        private isInRange(origin : number, percent : number, value : number) : boolean
        {
            return value >= origin * (1 - (percent / 100)) && value <= origin * (1 + (percent / 100))
        }

        private async Load(_img : string) : Promise<HTMLImageElement | null>
        {
            try 
            {
                const file = await fetch(_img);
                const blob = await file.blob();
                const objectURL = URL.createObjectURL(blob);
                const img = new Image();
                img.src = objectURL;
        
                return new Promise<HTMLImageElement>((resolve, reject) => {
                    img.onload = () => {
                        return resolve(img)
                    }

                    img.onerror = () => {
                        return null
                    }
                })
            }
            catch
            {
                return null;
            }
        }
        
        private async AVG(_img : HTMLImageElement) : Promise<string> 
        {
            try 
            {
                const crt = document.createElement('canvas');
                const context = crt.getContext("2d");
        
                if (context) 
                {
                    crt.width = _img.naturalWidth;
                    crt.height = _img.naturalHeight;

                    let data, length;
                    let i = -4, count = 0;
                    try 
                    {
                        context.drawImage(_img, 0, 0);
                        data = context.getImageData(0, 0, _img.naturalWidth, _img.naturalHeight);
                        length = data.data.length;
                    } 
                    catch
                    {
                        return "#FFFFFF";
                    }

                    let R = 0, G = 0, B = 0;
                    while ((i += this._ratio * 4) < length) {
                        ++count;
                        R += data.data[i]     
                        G += data.data[i + 1]    
                        B += data.data[i + 2]
                    }

                    R = Math.floor(R / count);
                    G = Math.floor(G / count);
                    B = Math.floor(B / count);
                    return TinyColor.RGBToHEX([R, G, B])
                } 
                else 
                {
                    return "#FFFFFF";
                }
            }
            catch
            {
                return "#FFFFFF";
            }
        }

        private async MED(_img: HTMLImageElement) : Promise<string> 
        {
            try 
            {
                const canvas = document.createElement('canvas');
                const context = canvas.getContext("2d");
                if (!context) 
                {
                    return "#FFFFFF";
                }
        
                canvas.width = _img.naturalWidth;
                canvas.height = _img.naturalHeight;
                if (canvas.width === 0 || canvas.height === 0) 
                {
                    return "#FFFFFF";
                }
        
                context.drawImage(_img, 0, 0);
                const data = context.getImageData(0, 0, canvas.width, canvas.height).data;
        
                let freq: { [key: string]: number } = {};
                let max = 0;
                let res = "#FFFFFF";
        
                for (let i = 0; i < data.length; i += this._ratio * 4) {
                    const R = data[i];
                    const G = data[i + 1];
                    const B = data[i + 2];
                    const alpha = data[i + 3];
        
                    if (alpha === 0) continue
        
                    const hex = TinyColor.RGBToHEX([R, G, B]);
                    freq[hex] = (freq[hex] || 0) + 1;
        
                    if (freq[hex] > max) {
                        max = freq[hex];
                        res = hex;
                    }
                }
                return res

            } 
            catch (error) 
            {
                return "#FFFFFF";
            }
        }

        private async ALL(img : HTMLImageElement) : Promise<string[]>
        {
            return new Promise((resolve, reject) => {
                const canvas = document.createElement('canvas');
                const ctx = canvas.getContext('2d');
                if (!ctx) 
                {
                    return ["#FFFFFF"]
                }
                canvas.width = img.width;
                canvas.height = img.height;
                ctx.drawImage(img, 0, 0, img.width, img.height);
                const imageData = ctx.getImageData(0, 0, img.width, img.height);
                const data = imageData.data;
            
                const colorSet = new Set<string>();
            
                for (let i = 0; i < data.length; i += 4) 
                {
                    const r = data[i];
                    const g = data[i + 1];
                    const b = data[i + 2];
                    const rgbColor = TinyColor.RGBToHEX([r,g,b]);
                    colorSet.add(rgbColor);
                }
            
                resolve(Array.from(colorSet));
            });
        }

        public async Pick(Img: string | HTMLImageElement) : Promise<TinyColor> 
        {
            let res : HTMLImageElement | null
            if(typeof Img == "string")
            {
                res = await this.Load(Img)
                if(res == null) return new TinyColor("#FFFFFF")
            }
            else
            {
                res = Img
            }
            const avgResult = await this.MED(res)
            let result = TinyColor.HEXToRGB(avgResult)
            if (this.isInRange(result[0], 10, result[1]) && this.isInRange(result[1], 10, result[2])) 
            {
                const medResult = await this.AVG(res)
                return new TinyColor(medResult)
            } 
            else 
            {
                return new TinyColor(avgResult)
            }
        }

        public async PickAll(Img : string | HTMLImageElement) : Promise<TinyColor[]>
        {
            let res : HTMLImageElement | null
            if(typeof Img == "string")
            {
                res = await this.Load(Img)
                if(res == null) return [new TinyColor("#FFFFFF")]
            }
            else
            {
                res = Img
            }
            const arr = await this.ALL(res)
            let result : TinyColor[] = []
            for(let i = 0; i<arr.length;i)
            {
                result.push(new TinyColor(arr[i]))
                i += Schell*this._ratio
            }

            return result
        }
        
        public async PickPalette(Img : string | HTMLImageElement) : Promise<TinyColor[]>
        {
            let res : HTMLImageElement | null
            if(typeof Img == "string")
            {
                res = await this.Load(Img)
                if(res == null) return [new TinyColor("#FFFFFF")]
            }
            else
            {
                res = Img
            }
            const arr = await this.ALL(res)
            let result : TinyColor[] = []
            result.push(new TinyColor((await this.Pick(res)).Hex()))
            let alr = [0,1]
            for(let i = 2; i<this._count;i++)
            {
                const n = Math.floor((Math.random() * ((arr.length-2)-1)+1))
                if(alr.indexOf(n) == -1)
                {
                    result.push(new TinyColor(arr[n]))
                    alr.push(n)
                    continue
                }
                else
                {
                    i--
                }
            }
            return result
        }

        public async PickFromPoint(Img : HTMLImageElement, event : MouseEvent) : Promise<TinyColor>
        {
            const canvas = document.createElement('canvas');
            const ctx = canvas.getContext('2d');
        
            if (!ctx || !(Img instanceof HTMLImageElement) || !(event instanceof MouseEvent)) {
            return new TinyColor("#FFFFFF")
            }
        
            canvas.width = Img.width;
            canvas.height = Img.height;
        
            ctx.drawImage(Img, 0, 0, Img.width, Img.height);
            const rect = Img.getBoundingClientRect();
            const x = event.clientX - rect.left;
            const y = event.clientY - rect.top;
        
            const pixelData = ctx.getImageData(x, y, 1, 1).data;
            const [r, g, b] = pixelData;
        
            return new TinyColor([r,g,b])
        }
    }
}