import gulp from 'gulp'
import exec from 'gulp-exec';
var options = { continueOnError: false, pipeStdout: false, };
var reportOptions = { err: false, stderr: true, stdout: true };

/**
 * PixstockAppのAngular2が使用する共通ライブラリのビルド
 */
gulp.task('build_angular_lib', () => {
    return gulp.src('gulpfile.babel.js')
        .pipe(exec('ngc -p ../src/PixstockApp/Pixstock.Nc.App.Core', options))
        .pipe(exec.reporter(reportOptions));
});

/**
 * PixstockAppのAngular2モジュールのビルド
 */
gulp.task('build_angular_app', () => {
    return gulp.src('gulpfile.babel.js')
        .pipe(exec('npm run install --prefix ./../src/PixstockApp/Pixstock.Nc.App.Pioneer', options))
        .pipe(exec('npm run install --prefix ./../src/PixstockApp/Pixstock.Nc.Stella', options))
        .pipe(exec('npm run ng build --prefix ./../src/PixstockApp/Pixstock.Nc.App.Pioneer', options))
        .pipe(exec('npm run ng build --prefix ./../src/PixstockApp/Pixstock.Nc.Stella', options))
        .pipe(exec.reporter(reportOptions));
});

gulp.task('setup_pixstock', () => {
    return gulp.src('gulpfile.babel.js')
        .pipe(exec('npm install --prefix ./../src/PixstockApp/Pixstock.Nc.App.Core', options))
        .pipe(exec.reporter(reportOptions));
});

