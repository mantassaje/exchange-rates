import React from 'react';
import DatePicker from 'react-datepicker';
import moment from 'moment';

import 'react-datepicker/dist/react-datepicker.css';

class DatePickerExchangeRatesFetcher extends React.Component {
    constructor(props) {
      super(props);
      this.state = {
        onDataLoaded: props.onDataLoaded,
        onError: props.onError
      };
    }

    
    componentDidMount() {
        var date = this.props.startValue;
        if(date !== null) {
            this.loadData(date);
        }
    }
  
    loadData = (date) => {
        if(!date) return;
        this.setState({
            date: date
        });
        const formattedDate = date && date.format("YYYY-MM-DD");
        const { onDataLoaded, onError } = this.state;
        fetch("http://localhost:64531/api/exchangeRates?date=" + formattedDate)
            .then(res => res.json())
            .then(
                (body) => {
                    if(body.message) onError(body);
                    else onDataLoaded(body);
                },
                (error) => {
                    onError(error);
                }
        )
    }
  
    render() {
        return (
        <div className="datePickerBlock">
            <label>Select date: </label>
            <div className="datePicker">
            <DatePicker 
                selected={this.state.date}
                onChange={this.loadData}
                dateFormat="YYYY-MM-DD"
                maxDate={new moment("2014-12-31")}
                placeholderText="Click to select a date"
            />
            </div>
        </div>);
      }
  }
  
  export default DatePickerExchangeRatesFetcher;