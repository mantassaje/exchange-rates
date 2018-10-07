import React, { Component } from 'react';
import './App.css';
import ExchangeRatesTable from './Components/ExchangeRatesTable.js'
import DatePickerExchangeRatesFetcher from './Components/DatePickerExchangeRatesFetcher'
import moment from 'moment';

class App extends Component {
  constructor(props) {
    super(props);
    this.state = {
      error: null,
      exchangeRates: [],
    };
  }


  render() {
    const { exchangeRates } = this.state;
    console.dir(exchangeRates);
    return (
      <div className="app">
        <h1>Exchange Rates of LTL</h1>
        <DatePickerExchangeRatesFetcher startValue={moment("2014-05-05")} onDataLoaded={this.onDataLoaded} onError={this.onError}></DatePickerExchangeRatesFetcher>
        {this.renderError()}
        <ExchangeRatesTable exchangeRates={exchangeRates}></ExchangeRatesTable>
      </div>
    );
  }

  renderError(){
    const {error} = this.state;
    if(error != null){
      return <div>{error.message}</div>
    }
  }

  onDataLoaded = (exchangeRates) => {
    console.dir("onDataLoaded");
    this.setState({
      exchangeRates: exchangeRates,
      error: null,
    })
  }

  onError = (error) => {
    console.dir("onError");
    this.setState({
      exchangeRates: [],
      error: error,
    })
  }
}

export default App;
