class ResultCollection extends React.Component {

    constructor(props) {
        super(props);

        this.resultEntries = this.props.resultEntries.map((result) => (
            <ResultEntry key={result.resultOrder} resultOrder={result.resultOrder} result={result.result} />
        ));
    }

    componentWillReceiveProps() {
        this.resultEntries = this.props.resultEntries.map((result) => (
            <ResultEntry key={result.resultOrder} resultOrder={result.resultOrder} result={result.result} />
        ));
    }

    render() {
        return (
            <table className="table table-bordered table-striped">
                <thead>
                    <tr>
                        <th>ลำดับรางวัล</th>
                        <th>ผลรางวัล</th>
                    </tr>
                </thead>
                <tbody>
                    {this.resultEntries}
                </tbody>
            </table>
            );
    }

}