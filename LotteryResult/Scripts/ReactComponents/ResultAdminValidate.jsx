class ResultAdminValidate extends React.Component {

    constructor(props) {
        super(props);

        this.state = {
            resultEntries: [],
            resultOrder: 1
        };

        this.resultEntries = this.state.resultEntries.map((result) => (
            <div className="col-md-6">
                <ResultCard key={result.username} result={result} />
            </div>
        ));

        this.validateResult = this.validateResult.bind(this);
    }

    validateResult() {
        $.ajax({
            url: "ValidateResult",
            method: "GET",
            data: {
                round: this.props.round.id,
                reward_type: this.props.rewardType.id,
                resultOrder: this.state.resultOrder
            }
        }).success((data, textStatus, jqXHR) => {
            if (data.status == 'ok') {
                switch (data.message) {
                    case 'wait-for-input':
                        this.setState({
                            resultEntries: []
                        });

                        window.setTimeout(this.validateResult, 2000);
                        break;
                    case 'one-user-input':
                        this.setState({
                            resultEntries: [
                                data.payload
                            ]
                        });

                        window.setTimeout(this.validateResult, 2000);
                        break;
                    case 'result-is-valid':
                        this.props.addResultEntry(this.state.resultOrder, data.payload.result_data_a.result);

                        if (this.state.resultOrder < this.props.rewardType.instance) {
                            this.setState((prevState, props) => ({
                                resultEntries: [
                                    data.payload.result_data_a,
                                    data.payload.result_data_b
                                ],
                                resultOrder: prevState.resultOrder + 1
                            }));

                            window.setTimeout(this.validateResult, 4000);
                        }
                        else {

                            window.setTimeout(() => {
                                window.location.href = '/Result/Create';
                            }, 4000);
                            
                        }

                        break;
                    case 'correction-required':

                        break;
                }
            }
        });
    }

    componentDidMount() {
        window.setTimeout(this.validateResult, 2000);
    }

    componentWillUpdate() {
        this.resultEntries = this.state.resultEntries.map((result) => (
            <div className="col-md-6">
                <ResultCard key={result.username} result={result} />
            </div>
        ));
    }

    render() {
        return (
            <div className="panel panel-default">
                <div className="panel-heading">ตรวจสอบผลรางวัล</div>
                <div className="panel-body">
                    {this.resultEntries}
                </div>
            </div>
            );
    }
}