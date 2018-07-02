class ResultEntryInput extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            status: 'allow-next-input',
            result: '',
            resultOrder: 1,
            btnDisabled: true,
            formValidation: 'default'
        };

        this.insertResult = this.insertResult.bind(this);
        this.allowNextInput = this.allowNextInput.bind(this);
        this.validateInput = this.validateInput.bind(this);
    }

    validateInput(event) {
        this.setState({ result: event.target.value });
        if (event.target.value.length == this.props.rewardType.format.length) {
            this.setState({ btnDisabled: false, formValidation: 'success' });
        }
        else {
            this.setState({ btnDisabled: true, formValidation: 'warning' });
        }
    }

    allowNextInput() {
        $.ajax({
            url: "AllowNextInsert",
            method: "GET",
            data: {
                round: this.props.round.id,
                reward_type: this.props.rewardType.id,
                resultOrder: this.state.resultOrder
            }
        }).success((data, textStatus, jqXHR) => {
            if (data.status == 'ok' && data.message == 'allow-next-input') {

                if (this.state.resultOrder < this.props.rewardType.instance) {
                    this.setState((prevState, props) => ({
                        status: 'allow-next-input',
                        resultOrder: prevState.resultOrder + 1,
                        result: '',
                        formValidation: 'default'
                    }));
                }
                else {
                    window.setTimeout(() => {
                        this.props.toggleState();
                    }, 4000);
                    
                }

            }
            else if (data.status == 'ok' && data.message == 'wait') {
                window.setTimeout(this.allowNextInput, 2000);
            }
        });
    }

    insertResult() {
        $.ajax({
            url: "InsertResult",
            method: "POST",
            data: {
                round: this.props.round.id,
                reward_type: this.props.rewardType.id,
                resultOrder: this.state.resultOrder,
                result: this.state.result
            },
            beforeSend: (jqXHR, settings) => {

                // Prevent repeating requests
                this.setState({ btnDisabled: true });
            }
        }).success((data, textStatus, jqXHR) => {
            if (data.status == 'ok' && data.message == 'result-inserted-successfully') {

                this.props.addResultEntry(this.state.resultOrder, this.state.result);

                this.setState({
                    status: 'wait'
                });

                window.setTimeout(this.allowNextInput, 2000);
            }
        });
    }

    render() {
        let statusText;
        let icon;
        let txtDisabled;
        if (this.state.status == 'allow-next-input') {
            statusText = 'พร้อมกรอกผลรางวัล';
            icon = 'glyphicon glyphicon-pencil';
            txtDisabled = false;
        }
        else if (this.state.status == 'wait') {
            statusText = 'รอตรวจสอบผลรางวัล';
            icon = 'glyphicon glyphicon-time';
            txtDisabled = true;
        }

        let inputGroupClass;
        let btnClass;
        if (this.state.formValidation == 'default') {
            inputGroupClass = 'input-group';
            btnClass = 'btn btn-default';
        }
        else if (this.state.formValidation == 'warning') {
            inputGroupClass = 'input-group has-warning';
            btnClass = 'btn btn-warning';
        }
        else  if (this.state.formValidation == 'success') {
            inputGroupClass = 'input-group has-success';
            btnClass = 'btn btn-success';
        }

        return (
            <div className="panel panel-default">
                <div className="panel-heading">ผลรางวัล</div>
                <div className="panel-body">
                    <div className={inputGroupClass}>
                        <input type="text" className="form-control" placeholder="ผลรางวัล" style={{ maxWidth: '100%' }} value={this.state.result} onChange={this.validateInput} disabled={txtDisabled} />
                        <span className="input-group-btn">
                            <button className={btnClass} type="button" onClick={this.insertResult} disabled={this.state.btnDisabled}>บันทึก</button>
                        </span>
                    </div>
                    <span className={icon} aria-hidden="true" style={{marginTop: '5px'}}></span> {statusText}
                </div>
            </div>

            );
    }
}