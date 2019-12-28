var vn = (function (env) {
    "use strict";

    /**
     * @summary Script default variables.
     */
    let defaults = {
        userAgentPattern: /(MSIE|Trident|(?!Gecko.+)Firefox|(?!AppleWebKit.+Chrome.+)Safari(?!.+Edge)|(?!AppleWebKit.+)Chrome(?!.+Edge)|(?!AppleWebKit.+Chrome.+Safari.+)Edge|AppleWebKit(?!.+Chrome|.+Safari)|Gecko(?!.+Firefox))(?: |\/)([\d\.apre]+)/i,
        api: {
            schema: "http",
            host: "localhost",
            port: "5000",
            pathname: "api/v1/analytics"
        }
    };

    /**
     * @summary Elements rcurrently rendered on page load.
     */
    const elements = {
        title: env.document.querySelector("title"),
        button: env.document.querySelector("input")
    };

    /**
     * @summary Query string data.
     */
    const page = (function () {
        const search = (env.location.search || "").substring(1);
        const params = search ? search.split("&") : [];
        let vars = [];

        if (params.length) {
            vars = params.reduce(function (map, items) {
                const pairs = items.split("=");
                const key = pairs[0];

                // If the pair doesn't already exist as a key in the object, map it.
                if (!map.hasOwnProperty(key)) {
                    map[key] = [];
                }

                map[key].push(pairs[1]);

                return map;
            }, {});
        }

        /**
         * @summary Creates page information, necessary for an analytics hit on the server.
         * @returns {Object} Page information
         */
        function createPageInformationMap() {
            let userAgent = defaults.userAgentPattern.exec(env.navigator.userAgent);
            const version = userAgent[2];
            const name = userAgent[1];

            return {
                pageName: elements.title.text || "",
                vendor: {
                    name: name,
                    version: version
                },
                parameters: vars
            };
        }

        return createPageInformationMap();
    }());

    async function hit() {
        return await fetch(`${defaults.api.schema}://${defaults.api.host}:${defaults.api.port}/${defaults.api.pathname}`, {
            method: "POST",
            credentials: "omit",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(page)
        });
    }

    elements.button.addEventListener("click", (e) => hit());

    return {
        analytics: {
            hit: async () => {
                console.debug(page);

                return hit();
            }
        }
    };

}(this));
