/*
    Copyright 2019 Thiago J. Xavier

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

        http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
*/

/* jshint esversion: 8 */

/**
 * @summary Analytics wrapper to hit server with basic page information.
 */
(function (env) {
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
     * @summary Elements currently rendered on page load.
     */
    const elements = {
        title: env.document.querySelector("title")
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
                const value = pairs[1];

                // If the pair doesn't already exist as a key in the object, map it.
                if (!map.hasOwnProperty(key)) {
                    map[key] = [];
                }

                map[key].push(value);

                return map;
            }, {});
        }

        /**
         * @summary Creates page information, necessary for an analytics hit on the server.
         * @returns {Object} Page information
         */
        function createPageInformationMap() {
            const userAgent = defaults.userAgentPattern.exec(env.navigator.userAgent);
            const version = userAgent[2] || "";
            const name = userAgent[1] || "";
            const pageName = elements.title.text || "";

            return {
                pageName: pageName,
                vendor: {
                    name: name,
                    version: version
                },
                parameters: vars
            };
        }

        return createPageInformationMap();
    }());

    /**
     * @summary Sends to server analytics information of current page.
     * @returns {Promise} A promise containing data about the server response.
     */
    async function hit() {
        return await env.fetch(`${defaults.api.schema}://${defaults.api.host}:${defaults.api.port}/${defaults.api.pathname}`, {
            method: "POST",
            credentials: "omit",
            headers: {
                "Content-Type": "application/json"
            },
            body: env.JSON.stringify(page)
        });
    }

    return {
        analytics: {
            hit: hit
        }
    };

}(this))
    .analytics
    .hit()
    .then(() => console.info("Analytics hit sent sucessfully."))
    .catch(() => console.warn("API is not running."));
