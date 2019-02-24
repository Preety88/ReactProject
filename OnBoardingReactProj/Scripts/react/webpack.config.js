module.exports = {
	mode: 'development',
	context: __dirname,
	entry: {
		Home: "./index.js",
		Customers: "./Customers.js",
		Products: "./Products.js",
		Sales: './Sales.js',
		Stores: './Stores.js'
	},
	output: {
		path: __dirname + "/dist",
		filename: "[name].bundle.js"
	},
	watch: true,
	module: {
		rules: [{
			test: /\.jsx?$/,
			exclude: /(node_modules)/,
			use: {
				loader: 'babel-loader',
				options: {
					presets: ['babel-preset-env', 'babel-preset-react']
				}
			}
		}]
	}
}
