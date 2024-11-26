namespace TinyObjects
{
    interface TinyData
    {
        [key : string] : any
    }

    class TinyStorage
    {
        public alpha : string
        private dom : boolean
        private data : TinyData
        private temp: string
        private debug: boolean
        private check : any

        constructor(alfabeto: string, debug: boolean)
        {
            this.alpha = alfabeto == null ? "" : alfabeto
            this.dom = (document == null) ? false : true
            this.debug = (debug == null || typeof (debug) != "boolean") ? false : debug
            this.temp = ""
            this.data = {}
        }
        
        private _init()
        {
            try
            {
                if(this.dom)
                {
                    this.data = this._get("Storage")
                    localStorage.removeItem("Storage")
                    this.check = setInterval(this._save, 5)
                    window.addEventListener("pagehide", () => {
                        clearInterval(this.check)
                        localStorage.removeItem(this.temp)
                        this._set("Storage")
                    })
                    window.addEventListener("pageshow",() => {
                        this._get("Storage")
                        localStorage.removeItem("Storage")
                        this.check = setInterval(this._save, 5)
                    })
                    return
                }
                throw new Error("Not in a DOM place")
            }
            catch
            {
                throw new Error("Init failed")
            }
        }

        private _save(stringa : string)
        {
            this.data = this._get(this.temp)
            localStorage.removeItem(this.temp)
            for(let i = 0; i<this.alpha.length; i++)
            {
                this.temp += this.alpha[(Math.random() * (this.alpha.length - 1)) + 1]
            }
            localStorage.setItem(this.temp, JSON.stringify(this.data))
        }

        private _get(stringa: string)
        {
            const r = localStorage.getItem(stringa) || "";
            return JSON.parse(r) || {};
        }
        private _set(stringa: string)
        {
            localStorage.setItemItem(stringa || "",JSON.stringify(this.data || {}))
        }

        public Get(key : string)
        {
            try
            {
                return this.data[key]
            }
            catch
            {
                return null;
            }
        }

        public Set(key : string, dato : any)
        {
            try
            {
                this.data[key] = dato
            }
            catch
            {
                return null;
            }
        }
    }
}