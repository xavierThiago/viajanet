module.exports = function (grunt) {
    "use strict";

    require("google-closure-compiler")
        .grunt(grunt);

    grunt.initConfig({
        pkg: grunt.file.readJSON("package.json"),
        clean: {
            init: {
                src: ["dist/**"]
            },
            optimization: {
                src: ["dist/.tmp", "dist/js/*.js", "!dist/js/*.min.js"]
            }
        },
        copy: {
            files: {
                expand: true,
                cwd: "src/assets/js",
                src: "**/*.js",
                dest: "dist/js"
            }
        },
        concat: {
            debug: {
                files: {
                    "dist/js/bundles/<%= pkg.name %>.js": "src/assets/js/*.js"
                }
            },
            release: {
                files: {
                    "dist/js/bundles/<%= pkg.name %>.<%= (new Date()).getTime() %>.min.js": "src/assets/js/**/*.js"
                }
            }
        },
        csslint: {
            owner: {
                src: ["src/assets/css/*.css", "!src/assets/css/vendor/*.css", "!src/assets/css/**/*.min.css"]
            },
            all: {
                src: ["src/assets/css/**/*.css", "!src/assets/css/**/*.min.css"]
            }
        },
        cssmin: {
            release: {
                target: {
                    files: [{
                        expand: true,
                        cwd: "src/assets/css",
                        src: ["*.css", "!*.min.css", "!vendor/*.min.css"],
                        dest: "dist/css",
                        ext: ".<%= (new Date()).getTime() %>.min.css"
                    }]
                }
            }
        },
        jshint: {
            owner: {
                src: ["gruntfile.js", "src/assets/js/*.js", "src/tests/js/**/*.js"],
                options: {
                    globals: {
                        esversion: 8
                    }
                }
            },
            all: {
                src: ["gruntfile.js", "src/assets/js/**/*.js", "src/tests/js/**/*.js"],
                options: {
                    globals: {
                        esversion: 8
                    }
                }
            }
        },
        "closure-compiler": {
            target: {
                files: {
                    "dist/js/<%= pkg.name %>.<%= (new Date()).getTime() %>.min.js": ["src/assets/js/*.js", "!src/assets/js/vendor/*.js"]
                },
                options: {
                    compilation_level: "SIMPLE",
                    language_in: "ECMASCRIPT_2018",
                    language_out: "ECMASCRIPT_2018"
                }
            }
        },
        pug: {
            debug: {
                options: {
                    pretty: true,
                    data: {
                        debug: true
                    }
                },
                files: {
                    "dist/index.html": "src/assets/html/index.pug",
                    "dist/landing.html": "src/assets/html/landing.pug",
                    "dist/confirmation.html": "src/assets/html/confirmation.pug",
                    "dist/checkout.html": "src/assets/html/checkout.pug"
                }
            },
            release: {
                options: {
                    data: {
                        debug: false,
                        timestamp: "<%= (new Date()).getTime() %>"
                    }
                },
                files: {
                    "dist/index.html": "src/assets/html/index.pug",
                    "dist/landing.html": "src/assets/html/landing.pug",
                    "dist/confirmation.html": "src/assets/html/confirmation.pug",
                    "dist/checkout.html": "src/assets/html/checkout.pug"
                }
            }
        },
        htmlmin: {
            release: {
                options: {
                    removeComments: true,
                    collapseWhitespace: true
                },
                files: [{
                    expand: true,
                    cwd: "dist",
                    src: ["*.html", "**/*.html"],
                    dest: "dist"
                }]
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-contrib-copy");
    grunt.loadNpmTasks("grunt-contrib-concat");
    grunt.loadNpmTasks('grunt-contrib-csslint');
    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks("grunt-contrib-jshint");
    grunt.loadNpmTasks("grunt-contrib-pug");
    grunt.loadNpmTasks("grunt-contrib-htmlmin");

    grunt.registerTask("build:debug", "Copy all assets, untouched, to debug all assets.", [
        "clean:init",
        "jshint:owner",
        "csslint:owner",
        "copy",
        "pug:debug"
    ]);

    grunt.registerTask("build:debug-bundle", "Copy all assets, untouched, to debug all assets [JavaScript files are bundled (owner's only)].", [
        "clean:init",
        "jshint:owner",
        "csslint:owner",
        "pug:debug",
        "copy",
        "clean:optimization",
        "concat:debug"
    ]);

    grunt.registerTask("build:release", "Copy all assets, untouched, to debug all assets.", [
        "clean:init",
        "copy",
        "jshint:all",
        "closure-compiler",
        "clean:optimization",
        "csslint:all",
        "cssmin:release",
        "pug:release",
        "htmlmin:release"
    ]);
};
