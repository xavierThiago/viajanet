module.exports = function (grunt) {
    "use strict";

    require("google-closure-compiler").grunt(grunt);

    grunt.initConfig({
        pkg: grunt.file.readJSON("package.json"),
        concat: {
            options: {
                separator: ";"
            },
            dist: {
                src: ["src/**/*.js"],
                dest: "dist/js/<%= pkg.name %>.js"
            }
        },
        cssmin: {
            target: {
                files: [{
                    expand: true,
                    cwd: "src/assets/css",
                    src: ["*.css", "!*.min.css", "!vendor/*.min.css"],
                    dest: "dist/css",
                    ext: ".min.css"
                }]
            }
        },
        "closure-compiler": {
            target: {
                files: {
                    "dist/js/<%= pkg.name %>.<%= grunt.template.today(+new Date()) %>.min.js": ["src/assets/js/*.js", "!src/assets/js/vendor/*.js"]
                },
                options: {
                    compilation_level: "SIMPLE",
                    language_in: "ECMASCRIPT_2018",
                    language_out: "ECMASCRIPT_2018"
                }
            }
        },
        uglify: {
            options: {
                banner: "/*! <%= pkg.name %> <%= grunt.template.today(+new Date()) %> */\n"
            },
            dist: {
                files: {
                    "dist/<%= pkg.name %>.min.js": ["<%= concat.dist.dest %>"]
                }
            }
        },
        jshint: {
            files: ["gruntfile.js", "src/**/*.js", "test/**/*.js"],
            options: {
                globals: {
                    jQuery: true,
                    console: true,
                    module: true,
                    document: true,
                    esversion: 8
                }
            }
        }
    });

    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks("grunt-contrib-jshint");

    grunt.registerTask("test", ["jshint"]);
    grunt.registerTask("dist", ["jshint", "cssmin", "closure-compiler"]);
    grunt.registerTask("default", ["jshint", "closure-compiler"]);
};
