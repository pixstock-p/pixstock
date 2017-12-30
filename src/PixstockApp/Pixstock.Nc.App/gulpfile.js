var gulp = require('gulp'); // gulpを読み込む
var merge = require('merge-stream');

// 「uglify」タスクを定義する
gulp.task('cpy_module', function () {
    var bootstrap_css = gulp.src('./node_modules/bootstrap/dist/css/bootstrap.css')
        .pipe(gulp.dest('wwwroot/lib/bootstrap'));
    var bootstrap_js = gulp.src('./node_modules/bootstrap/dist/js/bootstrap.js')
        .pipe(gulp.dest('wwwroot/lib/bootstrap'));

    return merge(bootstrap_css, bootstrap_js);
});