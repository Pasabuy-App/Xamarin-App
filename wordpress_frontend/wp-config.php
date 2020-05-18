<?php
/**
 * The base configuration for WordPress
 *
 * The wp-config.php creation script uses this file during the
 * installation. You don't have to use the web site, you can
 * copy this file to "wp-config.php" and fill in the values.
 *
 * This file contains the following configurations:
 *
 * * MySQL settings
 * * Secret keys
 * * Database table prefix
 * * ABSPATH
 *
 * @link https://wordpress.org/support/article/editing-wp-config-php/
 *
 * @package WordPress
 */

// ** MySQL settings - You can get this info from your web host ** //
/** The name of the database for WordPress */
define( 'DB_NAME', 'pasabuy' );

/** MySQL database username */
define( 'DB_USER', 'admin' );

/** MySQL database password */
define( 'DB_PASSWORD', 'admin' );

/** MySQL hostname */
define( 'DB_HOST', 'localhost' );

/** Database Charset to use in creating database tables. */
define( 'DB_CHARSET', 'utf8mb4' );

/** The Database Collate type. Don't change this if in doubt. */
define( 'DB_COLLATE', '' );

/**#@+
 * Authentication Unique Keys and Salts.
 *
 * Change these to different unique phrases!
 * You can generate these using the {@link https://api.wordpress.org/secret-key/1.1/salt/ WordPress.org secret-key service}
 * You can change these at any point in time to invalidate all existing cookies. This will force all users to have to log in again.
 *
 * @since 2.6.0
 */
define( 'AUTH_KEY',         'lW7SiXPlQw<qE@r!<m_|o6q{Bh&NA&jVZ>7:=KKQyq4=eXsOlpFcR)pO`TC8{y8#' );
define( 'SECURE_AUTH_KEY',  'z&+5_h($qfgaM<i|w(mFA/t)j~eTi(:/8g#1#yGyhH3Gam-FWyW[&~:1r)|q9Zs%' );
define( 'LOGGED_IN_KEY',    'p baKq(.^+Wf4(ahCaRlftjZvtD3Y/xS`)E)t5V0)>4[)(vM}=FxPuD@uX}w0Frf' );
define( 'NONCE_KEY',        'wu0#C[TY4Q;}y4#zPM|>{#[Z-|Mh(j)xDj8|qn|]Y/G7IO[U9F9*ny7nm]rocf!n' );
define( 'AUTH_SALT',        '<QacMT9>nuAWlKR9;&<)6.L7PIAe(RItR5ISuaS~HDdkz$YF&RlCQOmEm$ACT6M8' );
define( 'SECURE_AUTH_SALT', '-JBXRb=r_7WT30`]CQ[`O.)06TNAt,Fyj7gvuPMw{LoCMU4V0ff>p@yT=y?ASMfk' );
define( 'LOGGED_IN_SALT',   'dReP;bb91ruC0=k-A*lv;iNAaU;v[G <u`U)iw)6:>}D!EHUO+t5QdUEgHG30 -X' );
define( 'NONCE_SALT',       'PU|6;v7I)eYI2497NeWH6EU1J/ EVgI~XO3IfaolZ[f(rCV8Xn8TI{XS<ib<g Ky' );

/**#@-*/

/**
 * WordPress Database Table prefix.
 *
 * You can have multiple installations in one database if you give each
 * a unique prefix. Only numbers, letters, and underscores please!
 */
$table_prefix = 'wp_';

/**
 * For developers: WordPress debugging mode.
 *
 * Change this to true to enable the display of notices during development.
 * It is strongly recommended that plugin and theme developers use WP_DEBUG
 * in their development environments.
 *
 * For information on other constants that can be used for debugging,
 * visit the documentation.
 *
 * @link https://wordpress.org/support/article/debugging-in-wordpress/
 */
define( 'WP_DEBUG', false );

/* That's all, stop editing! Happy publishing. */

/** Absolute path to the WordPress directory. */
if ( ! defined( 'ABSPATH' ) ) {
	define( 'ABSPATH', __DIR__ . '/' );
}

/** Sets up WordPress vars and included files. */
require_once ABSPATH . 'wp-settings.php';
