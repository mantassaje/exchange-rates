import React, { Component } from 'react';
import './App.css';
import ExchangeRatesTable from './Components/ExchangeRatesTable.js'
import DatePickerExchangeRatesFetcher from './Components/DatePickerExchangeRatesFetcher'
import moment from 'moment';

class App extends Component {
  constructor(props) {
    document.title = "Exchange Rates for LTL";
    super(props);
    this.state = {
      error: null,
      exchangeRates: [],
    };
  }


  render() {
    const { exchangeRates } = this.state;
    return (
      <div className="app">
        <h1>Exchange Rates of LTL</h1>
        <DatePickerExchangeRatesFetcher startValue={moment("2014-12-05")} onDataLoaded={this.onDataLoaded} onError={this.onError}></DatePickerExchangeRatesFetcher>
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
    this.setState({
      exchangeRates: exchangeRates,
      error: null,
    })
  }

  onError = (error) => {
    this.setState({
      exchangeRates: [],
      error: error,
    })
  }
}

export default App;
