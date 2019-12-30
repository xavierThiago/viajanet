module.exports = function (grunt) {
    "use strict";

    require("google-closure-compiler")
        .grunt(grunt);

    grunt.initConfig({
        pkg: grunt.file.readJSON("package.json"),
        csslint: {
            strict: {
                src: ["src/assets/css/*.css", "!src/assets/css/vendor/*.css"]
            }
        },
        cssmin: {
            target: {
                files: [{
                    expand: true,
                    cwd: "src/assets/css",
                    src: ["*.css", "!*.min.css", "!vendor/*.min.css"],
                    dest: "dist/css",
                    ext: ".<%= grunt.template.today(+new Date()) %>.min.css"
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
        jshint: {
            files: ["gruntfile.js", "src/assets/js/*.js", "test/**/*.js"],
            options: {
                globals: {
                    esversion: 8
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-contrib-csslint');
    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks("grunt-contrib-jshint");

    grunt.registerTask("test", ["jshint"]);
    grunt.registerTask("dist", ["jshint", "cssmin", "closure-compiler"]);
    grunt.registerTask("default", ["jshint", "closure-compiler"]);
};
