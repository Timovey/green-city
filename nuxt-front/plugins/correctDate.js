export default ({app}, inject) => {
    inject('correctDate', function(date, format = "YYYY-MM-DD"){
        if (date > "9999-12-31")  {
            return null
        }
        if(date) {
            return app.$moment(date).format(format)
        }
        else {
            return null
        }
    })
}
